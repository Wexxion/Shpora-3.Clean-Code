using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markdown
{
    public static class UsefulThings
    {
        public static string ConverToHtml(string htmlTag, string content) 
            => $"<{htmlTag}>{content}</{htmlTag}>";
        public static bool IsCorrectIndex(this string str, int i) => i >= 0 && i < str.Length;
        public static bool HasDefaultConstructor(this Type t) => t.GetConstructor(Type.EmptyTypes) != null;

        public static IEnumerable<Type> GetAllImplemetationsOf<TType>()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(TType).IsAssignableFrom(p) && p.Name != typeof(TType).Name);
        }

        public static IEnumerable<Func<TType>> GetDefaultConstuctorsOf<TType>()
        {
            return GetAllImplemetationsOf<TType>()
                .Where(t => t.HasDefaultConstructor())
                .Select(x => Expression.Lambda<Func<TType>>(Expression.New(x)).Compile());
        }
    }
}
