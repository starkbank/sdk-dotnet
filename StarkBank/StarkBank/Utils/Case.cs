using System.Text;

namespace StarkBank.Utils
{
    internal static class Case
    {
        internal static string UpperCamelToLowerCamel(string upperCamel)
        {
            return char.ToLowerInvariant(upperCamel[0]) + upperCamel.Substring(1);
        }

        internal static string LowerCamelToUpperCamel(string upperCamel)
        {
            return char.ToUpperInvariant(upperCamel[0]) + upperCamel.Substring(1);
        }

        internal static string CamelToKebab(string camel)
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
