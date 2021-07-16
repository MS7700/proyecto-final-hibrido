using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Fin_Hibrido.Dto
{
    public class IngresoDto : ApiDto
    {
        public override void SortList<T>(List<T> list)
        {
            string columnname = getSortColumn() == "id" ? "IdIngreso" : getSortColumn();
            var property = typeof(T).GetProperty(columnname);
            var multiplier = getSortOrder() == "ASC" ? 1 : -1;
            list.Sort((t1, t2) => {
                var col1 = property.GetValue(t1);
                var col2 = property.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
        }

        public void FilterList(List<TipoIngreso> list)
        {
            JObject json = getFilterJson();
            foreach (var x in json)
            {
                string key = x.Key;
                JToken value = x.Value;
                if (key == "id")
                {
                    if (value.Type == JTokenType.Integer)
                    {
                        list.RemoveAll(p => p.IdIngreso != (int)value);
                    }
                    else
                    {
                        int[] myValue = value.ToObject<int[]>();
                        List<TipoIngreso> temp = new List<TipoIngreso>();
                        foreach (int v in myValue)
                        {
                            temp.Add(list.Find(p => p.IdIngreso == v));
                        }
                        list.Clear();
                        list.AddRange(temp);
                    }

                }
                if (key == "Nombre")
                {
                    list.RemoveAll(p => !p.Nombre.Contains((string)value));
                }
                if (key == "DependeSalario")
                {
                    list.RemoveAll(p => !(p.DependeSalario == (bool)value));
                }
                if (key == "Estado")
                {
                    list.RemoveAll(p => !(p.Estado == (bool)value));
                }
            }
        }
    }
}