using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidations;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    /// <summary>
    /// Burada iş kodları yazılmalıdır. Aşağıda herhangi bir iş kodu eklenmediği için
    /// sadece dataaccess ile data çekilmiştir.
    /// </summary>
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecureOperetion("Product.First,Admin")]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5, "cinarmurat1991@gmail.com")]
        [CacheAspect(duration: 10)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        [SecureOperetion("Product.List,Admin", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(5, "cinarmurat1991@gmail.com")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        [SecureOperetion("Product.List,Admin", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(duration: 10)]
        [PerformanceAspect(5, "cinarmurat1991@gmail.com")]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        [SecureOperetion("Admin", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [ValidationAspect(typeof(ProductValidator), Priority = 2)]
        [CacheRemoveAspect("IProductService.Get")]//Pattern' i IProductService.Get olanları kaldırır.
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfCategoryIsEnabled(product.CategoryId));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.Added);
        }

        [SecureOperetion("Admin", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [ValidationAspect(typeof(ProductValidator), Priority = 2)]
        [CacheRemoveAspect("IProductService.Get")]//Pattern' i IProductService.Get olanları kaldırır.
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.Updated);
        }

        [SecureOperetion("Product.List,Admin", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted);
        }

        [TransactionAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);

            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productDal.Get(p => p.ProductName == productName) != null)
            {
                return new ErrorResult(Messages.ProductNameAllreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryIsEnabled(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);
            if (result.Data==null || !result.Data.Status)
            {
                return new ErrorResult(Messages.CategoryStatusNotEnable);
            }

            return new SuccessResult();
        }
    }
}
