using System;
using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

namespace System.Linq
{
    using Collections.Generic;

    public static class LinqEx
    {
        public static IEnumerable<IEnumerable<T>> Split<T>( this IEnumerable<T> list, int parts )
        {
            int i = 0;
            var splits = from item in list
                group item by i++ % parts
                into part
                select part.AsEnumerable();
            return splits;
        }
    }
}

namespace K2P.Core.Extensions
{
    public static class ListEx
    {
        public static void Shuffle<T>( this IList<T> list )
        {
            var n = list.Count;

            while( n > 1 )
            {
                n--;

                int k = Random.Range( 0, n + 1 );

                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetLast<T>( this IList<T> list )
        {
            if( list.IsNullOrEmpty() )
                return default( T );

            return list[list.Count - 1];
        }

        public static int GetLastIndex<T>( this ICollection<T> list ) => list.Count - 1;

        public static void Swap<T>( this IList<T> list, int a, int b )
        {
            T tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }

        public static bool IsNullOrEmpty<T>( this ICollection<T> list ) => ( list?.Count ?? 0 ) <= 0;

        /// <summary>
        /// Compiler can't handle it without suffix. :(
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyReadOnly<T>( this IReadOnlyCollection<T> list ) => ( list?.Count ?? 0 ) <= 0;

        public static bool IsInRange<T>( this ICollection<T> list, int i )
        {
            if( i < 0 || i >= list.Count )
                return false;

            return true;
        }

        public static string Print<T>( this List<T> list )
        {
            if( list == null )
                return "[List null]";

            var sb = new StringBuilder();

            sb.Append( $"[List Count: {list.Count} Capacity: {list.Capacity}" );

            for( int i = 0; i < list.Count; i++ )
                sb.Append( $"\n\t{i}. {list[i]}" );

            sb.Append( "];" );

            return sb.ToString();
        }

        public static string Print<T>( this IReadOnlyList<T> list )
        {
            if( list == null )
                return "[IReadOnlyList null]";

            var sb = new StringBuilder();

            sb.Append( $"[IReadOnlyList ({list.GetType()}) Count: {list.Count}" );

            for( int i = 0; i < list.Count; i++ )
                sb.AppendFormat( "\n\t{0}. {1}", i, list[i] );

            sb.Append( "];" );

            return sb.ToString();
        }

        public static void RemoveDuplicates<T>( this List<T> list )
        {
            var set = new HashSet<T>();

            for( int i = list.Count - 1; i >= 0; i-- )
            {
                if( set.Contains( list[i] ) )
                {
                    list.RemoveAt( i );
                }
                else
                {
                    set.Add( list[i] );
                }
            }
        }

        public static void RemoveDuplicatesNonAlloc<T>( this List<T> list, HashSet<T> set )
        {
            for( int i = list.Count - 1; i >= 0; i-- )
            {
                if( set.Contains( list[i] ) )
                {
                    list.RemoveAt( i );
                }
                else
                {
                    set.Add( list[i] );
                }
            }
        }

        /// <summary>
        /// Checks nulls and duplicates
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ValidateUniques<T>( this List<T> list )
        {
            var msg = new StringBuilder();

            if( list == null )
                msg.Append( $"{nameof( list )} == null\n" );
            else
                for( int i = 0; i < list.Count; i++ )
                {
                    if( list[i] == null )
                        msg.Append( $"{nameof( list )}[{i}] == null\n" );
                    else
                    {
                        for( int j = i + 1; j < list.Count; j++ )
                            if( list[i].Equals( list[j] ) )
                                msg.Append( $"{nameof( list )}[{i}] == {nameof( list )}[{j}]\n" );
                    }
                }

            return msg.ToString().Trim();
        }

        /// <summary>
        /// Checks nulls and duplicates
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ValidateUniques<T>( this IList<T> list, Func<T, T, bool> areEqual )
        {
            var msg = new StringBuilder();

            if( list == null )
                msg.Append( $"{nameof( list )} == null\n" );
            else
                for( int i = 0; i < list.Count; i++ )
                {
                    if( areEqual( list[i], default( T ) ) )
                        msg.Append( $"{nameof( list )}[{i}] == null\n" );
                    else
                    {
                        for( int j = i + 1; j < list.Count; j++ )
                            if( areEqual( list[i], list[j] ) )
                                msg.Append( $"{nameof( list )}[{i}] == {nameof( list )}[{j}]\n" );
                    }
                }

            return msg.ToString().Trim();
        }

        public static T GetNextWithWrap<T>( this IList<T> list, T item )
        {
            if( list.IsNullOrEmpty() )
                throw new ArgumentNullException();

            int idx = list.IndexOf( item );

            if( idx < 0 )
                return list[0];

            if( list.Count == 1 )
                return list[0];

            idx++;
            if( idx >= list.Count )
                idx = 0;

            return list[idx];
        }

        public static T GetPreviousWithWrap<T>( this IList<T> list, T item )
        {
            if( list.IsNullOrEmpty() )
                throw new ArgumentNullException();

            int idx = list.IndexOf( item );
            if( idx < 0 )
                throw new ArgumentException();

            if( list.Count == 1 )
                return list[0];

            idx--;
            if( idx < 0 )
                idx = list.GetLastIndex();

            return list[idx];
        }

        public static int IndexOf<T>( this IReadOnlyList<T> list, T item )
        {
            return ( (IList<T>)list ).IndexOf( item );
        }
    }
}
