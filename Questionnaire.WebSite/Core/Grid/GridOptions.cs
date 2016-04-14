using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using Questionnaire.WebSite.Core;

namespace Questionnaire.WebSite.Core.Grid
{
    /// <summary>
    /// Grid options
    /// </summary>
    [ModelBinder(typeof(GridModelBinder))]
    public class GridOptions
    {
        public GridOperations Operation { get; set; }
        public Boolean IsSearch { get; set; }
        public Int32 PageIndex { get; set; }
        public Int32 PageSize { get; set; }
        public String SortColumn { get; set; }
        public String SortOrder { get; set; }
        public Int64 ND { get; set; }
        public Filter Where { get; set; }

        /// <summary>
        /// Apply grid options to data
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="query">Query</param>
        /// <returns></returns>
        public Int32 Apply<T>(ref IQueryable<T> query) where T:class
        {
            //filtring
            if (IsSearch)
            {
                //And
                if (Where.groupOp == "AND")
                    foreach (var rule in Where.rules)
                        query = query.Where<T>(
                            rule.field, rule.data,
                            (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<T>()).AsQueryable();
                    foreach (var rule in Where.rules)
                    {
                        var t = query.Where<T>(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        temp = temp.Concat<T>(t);
                    }
                    //remove repeating records
                    query = temp.Distinct<T>();
                }
            }

            //count
            var count = query.Count();

            if (!String.IsNullOrWhiteSpace(SortColumn))
            {
                //sorting
                query = query.OrderBy(SortColumn, SortOrder);
            }
            else
            {
                var prop = query.ElementType.GetProperties().First();
                query = query.OrderBy(prop.Name, SortOrder);
            }
            //paging
            query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
            return count;
        }
        
        public GridResult<T> GetResult<T, TEntityType>(IQueryable<TEntityType> query, Func<TEntityType, T> resultSelector) where TEntityType : class where T : class
        {
            var count = Apply(ref query);

            var result = new GridResult<T>
            {
                total = count == 0 ? 1 : (Int32)Math.Ceiling((Double)count / PageSize),
                page = PageIndex,
                records = count,
                rows = query.Select(resultSelector).ToList()
            };
            return result;
        }

        public GridResult<T> GetResult<T, TEntityType>(IEnumerable<TEntityType> records, Func<TEntityType, T> resultSelector)
            where TEntityType : class
            where T : class
        {
            var count = records.Count();

            records = records.Skip((PageIndex - 1) * PageSize).Take(PageSize);

            var result = new GridResult<T>
            {
                total = count == 0 ? 1 : (Int32)Math.Ceiling((Double)count / PageSize),
                page = PageIndex,
                records = count,
                rows = records.Select(resultSelector).ToList()
            };
            return result;
        }

        public GridResult<T> GetResult<T, TEntityType>(Int32 count, IEnumerable<TEntityType> records, Func<TEntityType, T> resultSelector)
            where TEntityType : class
            where T : class
        {
            var result = new GridResult<T>
            {
                total = count == 0 ? 1 : (Int32)Math.Ceiling((Double)count / PageSize),
                page = PageIndex,
                records = count,
                rows = records.Select(resultSelector).ToList()
            };
            return result;
        }
    }
}