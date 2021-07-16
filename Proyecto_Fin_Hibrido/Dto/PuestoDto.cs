﻿using Newtonsoft.Json.Linq;
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

        public void FilterList(List<Puesto> list)
        {
            JObject json = getFilterJson();
            foreach(var x in json)
            {
                string key = x.Key;
                JToken value = x.Value;
                if(key == "id")
                {
                    if (value.Type == JTokenType.Integer)
                    {
                        list.RemoveAll(p => p.IdPuesto != (int)value);
                    }
                    else
                    {
                        int[] myValue = value.ToObject<int []>();
                        List<Puesto> temp = new List<Puesto>();
                        foreach (int v in myValue)
                        {
                            temp.Add(list.Find(p => p.IdPuesto == v));
                        }
                        list.Clear();
                        list.AddRange(temp);
                    }

                }
                if(key == "puesto")
                {
                    list.RemoveAll(p => !p.Puesto1.Contains((string)value));
                }
            }
        }
    }
}