using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] businessRules)
        {
            foreach (var result in businessRules)
            {
                if (!result.Success)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
