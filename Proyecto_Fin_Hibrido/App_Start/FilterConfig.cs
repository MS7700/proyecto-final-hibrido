using Proyecto_Fin_Hibrido.Filters;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Fin_Hibrido
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           //filters.Add(new CountHeaderFilter());
        }
    }
}
