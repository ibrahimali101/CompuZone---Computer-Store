using CompuZone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CompuZone.Domain.Extentions
{
    public static class IQuerableExtentions
    {
        public static IQueryable<T> IF<T>(this IQueryable<T> source, bool condition, Expression< Func<T, bool>> predicate) 
            where T : class
        {
            if (condition)
                return source.Where(predicate);
          
            return source;
        }

        public static IQueryable<T> FilterText<T>(this IQueryable<T> source, string TextSeach)
            where T : NamedEntity
        {
            if (string.IsNullOrEmpty(TextSeach))
                return source;

             return source.Where(a =>
                                          a.Name.ToLower().Contains(TextSeach.ToLower()) ||
                                          a.Description.ToLower().Contains(TextSeach.ToLower())
             );

        }

        public static IQueryable<T>  OrderGroupBy<T>(this IQueryable<T> source , 
            List<(bool condition , Expression<Func<T , object>>)> predicate ,
            bool IsDesc = false )
        {
            foreach (var item in predicate)
            {
                if (item.condition)
                {
                    if (IsDesc)
                        source = source.OrderByDescending(item.Item2);
                    else
                        source = source.OrderBy(item.Item2);
                }
            }
            return source;
        }
    }
}
