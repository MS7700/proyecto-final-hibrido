﻿using System.Web;
using System.Web.Mvc;

namespace Proyecto_Fin_Hibrido
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
