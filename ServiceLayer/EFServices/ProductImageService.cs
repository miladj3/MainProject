using DataLayer.Context;
using DomainClasses.Entities;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Product;

namespace ServiceLayer.EFServices
{
    public class ProductImageService : IProductImageService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ProductImage> _productImages;
        #endregion

        #region Constructor
        public ProductImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productImages = _unitOfWork.Set<ProductImage>();
        }
        #endregion

        #region Methods
        public void Insert(AddProductPicturesViewModel viewModel)
        {
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image1,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image2,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image3,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image4,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image5,
                ProductId = viewModel.ProductId
            });
        }

        public void Edit(IEnumerable<ProductImage> images)
        {
            foreach (ProductImage image in images)
                _unitOfWork.MarkAsChanged(image);
        }

        public IEnumerable<DomainClasses.Entities.ProductImage> GetImages(Int64 productId) =>
            _productImages.Include(a => a.Product)
            .Where(a => a.ProductId == productId).ToList();

        #endregion
    }
}
