using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMIS.Utlities.Helpers
{
   public  class ArabicPrepocessor
    {
       public string StripArabicWords(string field, string sValue)
       {

           //try
           //{
           //    Service1Client client = new Service1Client();
             
           //    return client.Makeprocessing(sValue, field);

           //}
           //catch
           //{
               return sValue;
           //}

       }
       public string StripArabicWords(string sValue)
       {

           //try
           //{
           //    Service1Client client = new Service1Client();

           //    return client.Makeprocessing(sValue, "العنوان");

           //}
           //catch
           //{
               return sValue;
           //}

       }
    }
}
