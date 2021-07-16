using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Fin_Hibrido.Dto
{
    public class ApiDto
    {
        public string sort { get; set; }
        public string range { get; set; }
        public string filter { get; set; }

        private string[] sortArray()
        {
            return sort.Substring(1, sort.Length - 2).Replace("\"", "").Split(',');
        }
        private int[] rangeArray()
        {
            
            return range.Substring(1, range.Length - 2).Split(',').Select(int.Parse).ToArray();
        }
        public int getRangeStart()
        {
            return rangeArray()[0];
        }
        public int getRangeEnd()
        {
            return rangeArray()[1];
        }
        public virtual void rangeList<T>(List<T> list)
        {
            int start = getRangeStart();
            int end = getRangeEnd();
            if(start < 0 || start > end || start > list.Count())
            {
                list.Clear();
                return;
            }
            if(end < list.Count())
            {
                list.RemoveRange(end, list.Count() - end);
            }
            list.RemoveRange(0, start);
        }
        public string getSortColumn()
        {
            return sortArray()[0];
        }
        public string getSortOrder()
        {
            return sortArray()[1];
        }
        public virtual void SortList<T>(List<T> list)
        {
            var property = typeof(T).GetProperty(getSortColumn());
            var multiplier = getSortOrder() == "ASC" ? 1 : -1;
            list.Sort((t1, t2) => {
                var col1 = property.GetValue(t1);
                var col2 = property.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
        }


    }
}