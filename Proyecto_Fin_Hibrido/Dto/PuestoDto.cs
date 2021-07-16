using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Fin_Hibrido.Dto
{
    public class PuestoDto : ApiDto
    {
        public override void SortList<T>(List<T> list)
        {
            string columnname = getSortColumn() == "id" ? "IdPuesto" : "Puesto1";
            var property = typeof(T).GetProperty(columnname);
            var multiplier = getSortOrder() == "ASC" ? 1 : -1;
            list.Sort((t1, t2) => {
                var col1 = property.GetValue(t1);
                var col2 = property.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
        }
    }
}