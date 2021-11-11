using UnityEngine;
using System.Collections.Generic;

public static class ArrayEx
{
    public static void Shuffle<T>( this T[] array )
    {
        var n = array.Length;

        while( n > 1 )
        {
            n--;

            int k = Random.Range( 0, n + 1 );

            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public static void Shuffle<T>( this T[] array, int count )
    {
        var n = count;

        while( n > 1 )
        {
            n--;

            int k = Random.Range( 0, n + 1 );

            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public static T GetLast<T>( this T[] array )
    {
        return array[array.Length - 1];
    }

    public static void Swap<T>( this T[] array, int a, int b )
    {
        T tmp = array[a];
        array[a] = array[b];
        array[b] = tmp;
    }

    public static bool IsNullOrEmpty<T>( this T[] array )
    {
        if( array != null && array.Length > 0 )
            return false;

        return true;
    }

    public static T GetClamped<T>( this T[] array, int index )
    {
        index = Mathf.Clamp( index, 0, array.Length - 1 );

        return array[index];
    }

    public static string Print<T>( this T[] array )
    {
        if( array == null )
            return "[Array null]";

        var sb = new System.Text.StringBuilder();

        sb.AppendFormat( "[Array Length: {0}", array.Length );

        for( int i = 0; i < array.Length; i++ )
            sb.AppendFormat( "\n\t{0}. {1}", i, array[i] );

        sb.Append( "];" );

        return sb.ToString();
    }

    public static bool Contains<T>( this T[] array, T item, int startIndex, int endIndex )
    {
        for( ;startIndex < endIndex; ++startIndex )
        {
            if( EqualityComparer<T>.Default.Equals( array[startIndex], item ) ) return true;
        }
        return false;
    }

    public static bool Contains<T>( this T[] array, T item ) => array.Contains( item, 0, array.Length );

    public static T FirstOrDefault<T>(this T[] array, System.Predicate<T> predicate)
    {
        for( int i = 0; i < array.Length; ++i ) if( predicate( array[i] ) ) return array[i];
        return default;
    }
}
