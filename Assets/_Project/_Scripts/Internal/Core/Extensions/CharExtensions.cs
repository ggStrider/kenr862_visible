namespace Internal.Core.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        public static void ValidateIntoDigit(ref char c, char replaceIfNotDigit = '0')
        {
            if (!c.IsDigit()) c = '0';
        }
    }
}