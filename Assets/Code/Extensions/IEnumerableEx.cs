using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class IEnumerableEx
{
    public static string Print<T>( this IEnumerable<T> collection )
    {
        if( collection == null )
            return string.Format( "[IEnumerable<{0}> null]", typeof( T ).Name );

        var sb = new StringBuilder();

        int count = collection.Count();

        sb.AppendFormat( "[IEnumerable<{0}> Count: {1}", typeof( T ).Name, count );

        if( count > 0 )
            sb.Append( "\n" );

        int i = 0;

        foreach( T obj in collection )
        {
            sb.AppendFormat( "\t{0}. {1}\n", i, obj );
            i++;
        }

        sb.Append( "];" );

        return sb.ToString();
    }
}
