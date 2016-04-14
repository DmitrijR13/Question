using System;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;

namespace Questionnaire.WebSite.Core
{
    /// <summary>
    /// helper class provides number of methods to deal with sorting and filtering by entity property name
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>Orders the sequence by specific column and direction.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="direction">null, empty, "asc" for ascending or anything other for descending</param>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, String sortColumn, String direction)
        {
            var methodName = string.Format("OrderBy{0}",
                string.IsNullOrEmpty(direction ) || direction.ToLower() == "asc" ? "" : "Descending");

            var parameter = Expression.Parameter(query.ElementType, "p");

            var memberAccess = sortColumn.Split('.').Aggregate<String, MemberExpression>(null, (current, property) => Expression.Property(current ?? (Expression)parameter, property));

            var orderByLambda = Expression.Lambda(memberAccess, parameter);

            var result = Expression.Call(
                       typeof(Queryable),
                       methodName,
                       new[] { query.ElementType, memberAccess.Type },
                       query.Expression,
                       Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }

        /// <summary>
        /// filter query by specified column name(property path of entity), value and comparision operation
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="query"></param>
        /// <param name="column">property name or path</param>
        /// <param name="value">value to test</param>
        /// <param name="operation">test operation</param>
        /// <returns>filtered query</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, String column, Object value, WhereOperation operation)
        {
            if (String.IsNullOrEmpty(column))
                return query;

            var parameter = Expression.Parameter(query.ElementType, "p");

            var memberAccess = column.Split('.').Aggregate<string, MemberExpression>(null, (current, property) => Expression.Property(current ?? (Expression)parameter, property));
            var underlyingType = Nullable.GetUnderlyingType(memberAccess.Type) ?? memberAccess.Type;
            var isNullable = memberAccess.Type.IsGenericType;
            //change param value type
            //necessary to getting bool from string
            var filter = Expression.Constant(Convert.ChangeType(value, underlyingType));
            var strValue = Convert.ToString(value);
            //switch operation
            Expression condition;
            LambdaExpression lambda = null;
            switch (operation)
            {
                    //equal ==
                case WhereOperation.Equal:
                    condition = Expression.Equal(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                    //not equal !=
                case WhereOperation.NotEqual:
                    condition = Expression.NotEqual(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                    //string.Contains()
                case WhereOperation.Contains:
                    var tsExp = memberAccess.Type == typeof (String)
                        ? (Expression) memberAccess
                        : Expression.Call(null, typeof (LinqExtensions).GetMethod("ConvertToString", new [] {memberAccess.Type}), new[] {memberAccess});
                    condition = Expression.Call(tsExp,typeof(string).GetMethod("Contains", new[] { typeof(String) }), new Expression[]{Expression.Constant(strValue, typeof(String))});

                    if (isNullable)
                        condition = Expression.And(Expression.NotEqual(memberAccess, Expression.Constant(null)),
                                                   condition);
                    
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                //string.Contains()
                case WhereOperation.Contains2:
                    var tsExpr = memberAccess.Type == typeof(String) 
                        ? (Expression)memberAccess 
                        : Expression.Call(null, typeof(LinqExtensions).GetMethod("ConvertToString", new[] { memberAccess.Type }), new[] { memberAccess });
                    condition = Expression.Call(tsExpr, typeof(string).GetMethod("Contains", new[] { typeof(String) }), new Expression[] { Expression.Constant(strValue, typeof(String)) });
                    if (isNullable)
                        condition = Expression.And(Expression.NotEqual(memberAccess, Expression.Constant(null)),
                                                   condition);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
            }

            var result = Expression.Call(
                   typeof(Queryable), "Where",
                   new[] { query.ElementType },
                   query.Expression,
                   lambda);

            return query.Provider.CreateQuery<T>(result);
        }
        
        #region ConvertToString overloads
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(object obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Double obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Int32 obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Int64 obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Boolean obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Double? obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Int32? obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Int64? obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Boolean? obj)
        {
            throw new NotSupportedException();
        }

        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Decimal? obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Decimal obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Byte obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(Byte? obj)
        {
            throw new NotSupportedException();
        }
        
        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(DateTime? obj)
        {
            throw new NotSupportedException();
        }

        [EdmFunction("SqlServer", "STR")]
        public static String ConvertToString(DateTime obj)
        {
            throw new NotSupportedException();
        }

        #endregion
    
    }
}