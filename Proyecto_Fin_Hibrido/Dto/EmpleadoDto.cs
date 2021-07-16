using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Fin_Hibrido.Dto
{
    public class EmpleadoDto : ApiDto
    {
        public override void SortList<T>(List<T> list)
        {
            string columnname = getSortColumn() == "id" ? "IdEmpleado" : getSortColumn();
            var property = typeof(T).GetProperty(columnname);
            var multiplier = getSortOrder() == "ASC" ? 1 : -1;
            list.Sort((t1, t2) => {
                var col1 = property.GetValue(t1);
                var col2 = property.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
        }

        public void FilterList(List<Empleado> list)
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
                        list.RemoveAll(p => p.IdEmpleado != (int)value);
                    }
                    else
                    {
                        int[] myValue = value.ToObject<int[]>();
                        List<Empleado> temp = new List<Empleado>();
                        foreach (int v in myValue)
                        {
                            temp.Add(list.Find(p => p.IdEmpleado == v));
                        }
                        list.Clear();
                        list.AddRange(temp);
                    }

                }
                if (key == "Cedula")
                {
                    list.RemoveAll(p => !p.Cedula.Contains((string)value));
                }
                if (key == "Nombre")
                {
                    list.RemoveAll(p => !p.Nombre.Contains((string)value));
                }
                if (key == "Salario")
                {
                    list.RemoveAll(p => !(p.Salario == (double)value));
                }
                if (key == "DepartamentoId")
                {
                    list.RemoveAll(p => !(p.IdDepartamento == (int)value));
                }
                if (key == "PuestoId")
                {
                    list.RemoveAll(p => !(p.IdPuesto == (int)value));
                }
                if (key == "NominaId")
                {
                    list.RemoveAll(p => !(p.IdNomina == (int)value));
                }
            }
        }
    }
}