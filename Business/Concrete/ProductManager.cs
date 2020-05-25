using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    /// <summary>
    /// Burada iş kodları yazılmalıdır. Aşağıda herhangi bir iş kodu eklenmediği için
    /// sadece dataaccess ile data çekilmiştir.
    /// </summary>
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> GetById(int productId)
        {
            try
            {
                return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId)); 
            }
            catch (Exception e)
            {
                return new ErrorDataResult<Product>("Error : " + e.Message);
            }
           
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p=>p.CategoryId==categoryId).ToList());
        }

        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.Added);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
