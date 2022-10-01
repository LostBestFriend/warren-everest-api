namespace DomainModels.ExtensionMethods
{
    public static class MyExtensions
    {
        public static string FormatString(this string str)
        {
            return str.Replace(".", "").Replace("-", "").Replace(",", "").Trim();
        }
    }
}
