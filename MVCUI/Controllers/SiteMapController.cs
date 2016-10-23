using MVCUI.Helpers;
using MVCUI.SiteMap;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.SiteMap;

namespace MVCUI.Controllers
{
    public partial class SiteMapController : Controller
    {
        #region Fields
        private readonly ICategoryService _category;
        private readonly IProductService _product;
        private readonly IPageService _page;
        #endregion

        #region Constracture
        public SiteMapController(ICategoryService category, IProductService product, IPageService page)
        {
            this._page = page;
            this._product = product;
            this._category = category;
        }
        #endregion

        [HttpGet]
        [Route("SiteMap.xml")]
        public virtual async Task<ActionResult> Index()
        {
            SiteMap.SiteMap _siteMap = new SiteMap.SiteMap();
            //    IList<String> controllers = ControllerScanner.ScanAllControllers(requestBase: Request);
            String[] nameAllController = { "product", "blog" };
            IEnumerable<SiteMapShowViewModel> model = null;
            IList<Int64> t = (IList<Int64>)await _category.GetCountCategoryForSiteMap();
            String url = Request.Url.GetLeftPart(UriPartial.Authority);

            foreach(Int64 item in t)
                _siteMap.Add(new DomainClasses.Entities.SiteMapEntity
                {
                    Url = $"{url}/search/category/{ item }/page/1/count/8000/filter/all/keyword",
                    ChangeFrequency = DomainClasses.Enums.ChangeFreq.Always,
                    LastModified = DateTime.UtcNow,
                    priority = 0.5f
                });

            foreach (String controller in nameAllController)
            {
                if (controller.ToLowerInvariant().Contains("product"))
                    model = await _product.GetAllForSiteMap();

                else if (controller.ToLowerInvariant().Contains("blog"))
                    model = await _page.GetAllForSiteMap();

                else
                    continue;

                foreach (SiteMapShowViewModel _model in model)
                    _siteMap.Add(new DomainClasses.Entities.SiteMapEntity
                    {
                        Url = $"{url}/{controller}/{_model.Id}/{_model.Name.GenerateSlug()}",
                        ChangeFrequency = DomainClasses.Enums.ChangeFreq.Always,
                        LastModified = DateTime.UtcNow,
                        priority = 0.5f
                    });
            }
            return new XmlResult(_siteMap);
        }
    }
}