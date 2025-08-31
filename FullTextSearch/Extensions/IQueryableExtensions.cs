using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace FullTextSearch.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> FullTextSearch<T>(this IQueryable<T> source, string searchTerm, Expression<Func<T, string>> propertySelector)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || propertySelector == null)
                return source;
            // Turkish and English special character normalization
            searchTerm = searchTerm.Trim().NormalizeTurkishCharactersToEnglishCharacters();
            var fullTextEnabled = true;
            var parameter = propertySelector.Parameters[0];
            Expression? combined = null;
            // Tek propertySelector için array oluşturup foreach ile dön
            foreach (var selector in new[] { propertySelector })
            {
                if (fullTextEnabled)
                {
                    var property = selector.Body;
                    var efFunctions = Expression.Property(null, typeof(EF), nameof(EF.Functions));
                    var containsMethod = typeof(Microsoft.EntityFrameworkCore.SqlServerDbFunctionsExtensions).GetMethod(
                        "Contains",
                        new[] { typeof(DbFunctions), typeof(string), typeof(string) });
                    var call = Expression.Call(
                        containsMethod!,
                        efFunctions,
                        property,
                        Expression.Constant("\"" + searchTerm + "*\""));
                    combined = combined == null ? call : Expression.OrElse(combined, call);
                }
                else
                {
                    var property = selector.Body;
                    var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var containsCall = Expression.Call(property, containsMethod!, Expression.Constant(searchTerm));
                    var andExpr = Expression.AndAlso(notNull, containsCall);
                    combined = combined == null ? andExpr : Expression.OrElse(combined, andExpr);
                }
            }
            var lambda = Expression.Lambda<Func<T, bool>>(combined!, parameter);
            return source.Where(lambda);
        }
    }
}
