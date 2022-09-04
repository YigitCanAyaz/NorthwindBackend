using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            // context'ini oluştur gönderilen entity'nin (Product olabilir) ve onu Validate et (kurallarına bak)
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            // Eğer kurallara uymuyorsa Error fırlat
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
