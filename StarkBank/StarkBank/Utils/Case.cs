using System.Text;

namespace StarkBank.Utils
{
    internal static class Case
    {
        internal static string PascalToCamel(string pascal)
        {
            return char.ToLowerInvariant(pascal[0]) + pascal.Substring(1);
        }

        internal static string CamelToPascal(string camel)
        {
            return char.ToUpperInvariant(camel[0]) + camel.Substring(1);
        }

        internal static string CamelOrPascalToKebab(string camel)
        {
            var builder = new StringBuilder();
            builder.Append(char.ToLower(camel[0]));

            foreach (var c in camel.Substring(1))
            {
                if (char.IsUpper(c))
                {
                    builder.Append('-');
                    builder.Append(char.ToLower(c));
                }
                else
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}
