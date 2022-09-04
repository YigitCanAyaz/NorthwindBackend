using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception // Aspect
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // Eğer IValidator değilse o zaman hata fırlat
            // defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        // OnBefore'u override ediyoruz nasıl çalışacağı ile ilgili
        protected override void OnBefore(IInvocation invocation)
        {
            // ProductValidator instance'ı oluştur, <> içini al yani Product'ı, daha sonra parametreden gelen eşit tipe olana eşitle, daha sonra foreach ile validate et (Parametrede birden fazla Product olabilir)
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // reflection (Mesela ProductValidator'un instanceını oluştur)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // ProductValidator'un çalışma tipini bul, base type: interface ve generic argümanından ilk indexini bul yani Product'ı
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // onun parametrelerini bul (ilgili metodun parametrelerini bul), invocation metodun parametrelerinde bak entityType'a eşit olanları çek
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
