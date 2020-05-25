using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage(Messages.ProductNameCanNotEmply);
            RuleFor(p => p.ProductName).Length(1, 40).WithMessage(Messages.ProductNameLength);
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage(Messages.CategoryIdCanNotEmpty);
            RuleFor(p => p.QuantityPerUnit).Length(0, 20).WithMessage(Messages.QuantityPerUnitLenght);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).WithMessage(Messages.UnitPriceNotEqualZero);
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage(Messages.UnitPriceCanNotEmpty);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(2000)
                .When(p => !String.IsNullOrEmpty(p.ProductName) && p.ProductName.Contains("Phone"))
                .WithMessage(Messages.PhonePriceIsGreaterThenX);
            RuleFor(p => p.ProductName).Must(NotIncludeSpecialCharacter).WithMessage(Messages.ProductNameNotIncludeSpecialChracter);

        }

        private bool NotIncludeSpecialCharacter(string arg)
        {
            if (!String.IsNullOrEmpty(arg))
            {
                char[] character = arg.ToCharArray();
                for (int i = 0; i < arg.Length; i++)
                {
                    if (!char.IsLetter(character[i]) && !char.IsNumber(character[i]) && character[i] != ' ')
                    {
                        return false;
                        break;
                    }
                }
            }

            return true;
        }
    }
}
