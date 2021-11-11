using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2P.Core.Extensions
{
    public static class CollectionsEx
    {
        public static string Print( this IEnumerable list )
        {
            string type = $"IEnumerable";

            if( list == null )
                return $"[{type} null]";

            var sb = new StringBuilder();

            int count = 0;
            foreach( var el in list )
                count++;

            sb.Append( $"[{type} Count: {count}" );

            int i = 0;

            foreach( var el in list )
                sb.Append( $"\n\t{i++}. {el}" );

            sb.Append( "];" );

            return sb.ToString();
        }

        public static string Print<T>( this IEnumerable<T> list )
        {
            string type = $"IEnumerable<{typeof( T ).Name}>";

            if( list == null )
                return $"[{type} null]";

            var sb = new StringBuilder();

            sb.Append( $"[{type} Count: {list.Count()}" );

            int i = 0;

            foreach( var el in list )
                sb.Append( $"\n\t{i++}. {el}" );

            sb.Append( "];" );

            return sb.ToString();
        }
    }
}
