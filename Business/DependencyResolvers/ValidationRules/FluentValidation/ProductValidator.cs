using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.DependencyResolvers.Autofac.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
           
            // Productın unitprice'ı 10'dan büyük veya eşit olduğu durumlarda ve Category 1'e eşit olduğu durumlarda Kuralı!
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);

            //ex: Product ismi A harfiyle başlamalıdır Kendi methodunu kural haline getirme
            //                        ->Must(Kendi Fonksiyonun ismi)
            //                        ->Fonksiyonunu Aşağıdaki gibi yaz
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Lütfen Ürün İsminin Başında A Harfi Kullanın");

            


        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}

