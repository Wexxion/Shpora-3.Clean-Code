using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Markdown.Lang;

namespace Markdown
{
    public static class UsefulThings
    {
        public static bool IsEscaped(this char? symbol) => '\\' == symbol;

        public static string ConverToHtml(this IToken token)
        {
            if (token is TagContent dataToken)
                return dataToken.content;
            var content = string.Join("", token.Content.Select(x => x.ConverToHtml()));
            if (token.IsClosed) 
                return ConverToHtml(token.HtmlTag, content);
            return token.MdTag + content;
        }
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
