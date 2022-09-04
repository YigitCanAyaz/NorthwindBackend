using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        // Araç olduğu için static
        // params verdiğimiz zaman Run içerisine istediğimiz kadar IResult verebiliyoruz 
        // logic => iş kuralı, params virgüllü parametreleri arka alanda array yapar
        // Başarısız olanları manager'a yolluyoruz (metodu) [kuralın kendisini]
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if(!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
