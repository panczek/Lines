using System.Text;

public static class StringBuilderEx
{
    public static void AppendIndented( this StringBuilder sb, string text, int level )
    {
        for( int i = 0; i < level; i++ )
            sb.Append( '\t' );

        sb.Append( text );
    }

    public static void AppendLineIndented( this StringBuilder sb, string text, int level )
    {
        for( int i = 0; i < level; i++ )
            sb.Append( '\t' );

        sb.AppendLine( text );
    }
}
