using System.Globalization;

public static class StringEx
{
    public static byte[] GetBytes( this string str )
    {
        return System.Text.Encoding.ASCII.GetBytes( str );
    }

    public static bool IsNullOrEmpty( this string str )
    {
        return string.IsNullOrEmpty( str );
    }

    public static bool Contains( this string str, string search, CompareOptions options, CultureInfo culture = null )
    {
        if( culture == null )
            culture = CultureInfo.InvariantCulture;

        return culture.CompareInfo.IndexOf( str, search, options ) >= 0;
    }
}
