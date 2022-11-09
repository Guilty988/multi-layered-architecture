using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.DependencyResolvers.Autofac.FluentValidation;
using Core.Aspect.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;


        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }

        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //Bir Kategoride en fazla 10 ürün ekle!
            //Busines codelar Yazılır
                IResult result =
                BusinessRules.Run
                (CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckProductNameExists(product.ProductName),
                CheckİfCategoryLimitExceded());


            if(result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


        }

        
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
           /* if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
           */
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(data => data.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();

        }

        //Kontrol Methodları

        private IResult CheckİfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitedExceded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // Bir kategoride 10 tane ürün olabilir kontrolü
            var result = _productDal.GetAll(data => data.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckProductNameExists(string name)
        {
            var result = _productDal.GetAll(data => data.ProductName == name).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        #region Add için Burayı Kontrol Et
        /*public IResult Add(Product product)
        {
            // ürünü eklemeden önce şartlara uyuyorsa eklenir
            // Business codelar burda yazılır
            // Validaton Kodları

            //Fluent Validation kullanımı
            //Bütün Projelerde bu doğrulamayı kullanmak için Core  Katmanına git CrossCuttingConcerns

           
            var context = new ValidationContext<Product>(product);
            ProductValidator productValidator = new ProductValidator();
            var result = productValidator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
           

            //Core Katmanı üzerinde ValidationTool Oluşturup içine productValidator ve product'ı Gönderiyoruz
            // ValidationTool.Validate(new ProductValidator(), product);
            //Birden fazla parametre varsa mesela
            //Loglama
            //Performanc
            //Transaction
            //Yetkilendirme vs vs
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


        }
    */

        #endregion

    }
}

