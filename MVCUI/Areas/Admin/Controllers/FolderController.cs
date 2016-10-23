using DataLayer.Context;
using DomainClasses.Entities;
using MVCUI.Filters;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Folder;

namespace MVCUI.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Folder")]
    [Route("{action}")]
    [T4MVC]
    [SiteAuthorize(Roles = "admin")]
    public partial class FolderController : Controller
    {
        #region Fiels
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFolderService _folder;
        private readonly IPictureService _picture;
        private readonly String _pathUpload = Links.BundleExtension.PathAbsolouteUploadPicture.PathUploadPicture;
        #endregion

        #region Constracture
        public FolderController(IUnitOfWork unitOfwork, IPictureService picture, IFolderService folder)
        {
            this._unitOfWork = unitOfwork;
            this._picture = picture;
            this._folder = folder;
        }
        #endregion

        #region Index
        [Route("FolderList")]
        [HttpGet]
        [OutputCache(CacheProfile ="long")]
        public virtual async Task<ActionResult> Index(String ElementId = "")
        {
            ViewBag.ElementId = ElementId;
            IEnumerable<Folder> folders = await _folder.GetList();
            if (Request.IsAjaxRequest())
                return PartialView(MVC.Admin.Shared.Views._FoldersLightBox, folders);
            return View(MVC.Admin.Folder.Views.Index, folders);
        }
        #endregion

        #region Create
        [OutputCache(CacheProfile ="long")]
        [HttpGet]
        [Route("Create-Folder")]
        public virtual ActionResult CreateFolder() => View();

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        [Route("Create-Folder")]
        public virtual async Task<ActionResult> CreateFolder(AddFolderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا دوباره اطلاعات را وارد نمایید");
                return View(MVC.Admin.Folder.Views.CreateFolder, model);
            }

            Boolean result = await _folder.Add(model);
            switch (result)
            {
                case true:
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(MVC.Admin.Folder.ActionNames.Index, MVC.Admin.Folder.Name);

                case false:
                    ModelState.AddModelError("Name", "این نام قبلا وارد شده است");
                    return View(MVC.Admin.Folder.Views.CreateFolder, model);

                default:
                    ModelState.AddModelError("", "لطفا دوباره اطلاعات را وارد نمایید");
                    return View(MVC.Admin.Folder.Views.CreateFolder, model);
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id:long}")]
        [OutputCache(CacheProfile ="long")]
        public virtual async Task<ActionResult> Edit(Int64? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditFolderViewModel model = await _folder.GetForEdit(id.Value);
            if (model == null)
                return HttpNotFound();

            return View(MVC.Admin.Folder.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> Edit(EditFolderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا مجددا تلاش کنید");
                return View(MVC.Admin.Folder.Views.Edit, model);
            }
            Boolean status = await _folder.Edit(model);
            switch (status)
            {
                case true:
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(MVC.Admin.Folder.ActionNames.Index, MVC.Admin.Folder.Name);

                case false:
                    ModelState.AddModelError("Name", "این نام قبلا وارد شده است");
                    return View(MVC.Admin.Folder.Views.Edit, model);

                default:
                    ModelState.AddModelError("", "لطفا مجددا تلاش کنید");
                    return View(MVC.Admin.Folder.Views.Edit, model);
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        [Route("DeleteFolder")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> DeleteFolder(Int64? Id)
        {
            if (Id == null)
                return Json(false);

            Array path = await _picture.GetPicturesOfFolder(Id.Value);
            await _folder.Delete(Id.Value);

            foreach (String item in path)
                if (System.IO.File.Exists(Server.MapPath(System.IO.Path.Combine(_pathUpload, item))))
                    System.IO.File.Delete(Server.MapPath(System.IO.Path.Combine(_pathUpload, item)));

            await _unitOfWork.SaveChangesAsync();
            return Json(true);
        }
        #endregion

        #region Picture
        #region Index Picture
        [HttpGet]
        [Route("{folderId:long}/Pictures")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> Pictures(Int64? folderId, String elementId = "")
        {
            ViewBag.ElementId = elementId;

            if (folderId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (await _folder.GetForEdit(folderId.Value) == null)
                return HttpNotFound();

            ViewBag.FolderId = folderId.Value;

            if (Request.IsAjaxRequest())
                return PartialView(MVC.Admin.Shared.Views._PictureLightBox);

            return View(MVC.Admin.Folder.Views.Pictures);
        }

        [HttpGet]
        [Route("List-Picture")]
        [OutputCache(Duration = 1, VaryByParam = "*")]
        [ChildActionOnly]
        public virtual PartialViewResult ListOfPicture(Int32 page = 1, Int64 folderId = 0)
        {
            Int32 total;
            IEnumerable<Picture> picture = _picture.GetAll(page, 10, out total, folderId);
            ViewBag.Total = total;
            ViewBag.PageNumber = page;
            ViewBag.FolderId = folderId;
            return PartialView(MVC.Admin.Folder.Views.ListOfPicture, picture);
        }
        #endregion

        #region Add Picture To Folder
        [HttpGet]
        [Route("{folderId}/Add-Picture-To-Folder")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> AddPictureToFolder(Int64? folderId)
        {
            if (folderId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (await _folder.GetForEdit(folderId.Value) == null)
                return HttpNotFound();
            ViewBag.FolderId = folderId.Value;
            return View(MVC.Admin.Folder.Views.AddPictureToFolder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{folderId}/add-Picture-To-Folder")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> AddPictureToFolder(IEnumerable<HttpPostedFileBase> files, Int64? folderId)
        {
            String[] allowedFiles =
            {
                 "image/gif",
                "image/jpeg",
                "image/jpg",
                "image/png"
            };

            if (folderId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpPostedFileBase[] receiverFiles = files as HttpPostedFileBase[] ?? files.ToArray();
            if (!receiverFiles.Any())
            {
                ModelState.AddModelError("", "مشکلی در انجام عملیات پیش آمده است");
                return View(MVC.Admin.Folder.Views.AddPictureToFolder);
            }

            if (!System.IO.Directory.Exists(Server.MapPath(_pathUpload)))
                System.IO.Directory.CreateDirectory(Server.MapPath(_pathUpload));

            foreach (HttpPostedFileBase item in receiverFiles)
            {
                if (item != null && !allowedFiles.Contains(item.ContentType))
                {
                    ModelState.AddModelError("", "این نوع فایل ها اجازه آپلود ندارد.");
                    return View(MVC.Admin.Folder.Views.AddPictureToFolder);
                }
                if (item == null && item.ContentLength <= 0)
                    continue;

                String fileName = Guid.NewGuid().ToString()+".png",
                    imagePath = Server.MapPath(System.IO.Path.Combine(_pathUpload, fileName));

                item.SaveAs(imagePath);
                Picture pic = new Picture
                {
                    Path = fileName,
                    FolderId = folderId.Value
                };
                _picture.Add(pic);
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Folder.ActionNames.Pictures, MVC.Admin.Folder.Name, new { folderId = folderId });
        }
        #endregion

        #region Delete
        [HttpPost]
        [Route("Delete-Picture")]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile ="nostore")]
        public virtual async Task<JsonResult> DeletePicture(Int64? id)
        {
            if (id == null)
                return Json(false);

            Picture pic =await  _picture.GetById(id.Value);
            if (pic == null)
                return Json(false);

            await _picture.Delete(id.Value);

            if (System.IO.File.Exists(Server.MapPath(System.IO.Path.Combine(_pathUpload, pic.Path))))
                System.IO.File.Delete(Server.MapPath(System.IO.Path.Combine(_pathUpload, pic.Path)));
            return Json(true);
        }
        #endregion

        #region Light Box
        [HttpGet]
        [OutputCache(Duration =1, VaryByParam ="*")]
        [Route("ListPicture")]
        public virtual PartialViewResult ListForLightBox(Int32 page =1, Int64 folderId =0)
        {
            Int32 total;
            IEnumerable<Picture> pic = _picture.GetAll(page: page, count: 18, total: out total, folderId: folderId);
            ViewBag.Total = total;
            ViewBag.PageNumber = page;
            ViewBag.FolderId = folderId;
            return PartialView(MVC.Admin.Shared.Views._PicturesList, pic);
        }
        #endregion
        #endregion

        #region Check Exist

        [HttpPost]
        [Route("CheckFolderNameExistForAdd")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckFolderNameExistForAdd(String Name) =>
            await _folder.CheckNameExist(Name) ?
            Json(false) :
            Json(true);

        [HttpPost]
        [Route("CheckFolderNameExistForEdit")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckFolderNameExistForEdit(String Name, Int64 id) =>
            await _folder.CheckNameExist(name: Name, id: id) ?
            Json(false) :
            Json(true);
        #endregion
    }
}