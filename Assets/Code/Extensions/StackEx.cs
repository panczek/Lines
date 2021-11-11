using System.Collections.Generic;

public static class StackEx
{
#if UNITY_EDITOR

    public static string Print<T>( this Stack<T> stack )
    {
        var sb = new System.Text.StringBuilder();
        var array = stack.ToArray();

        sb.AppendFormat( "Stack Count: {0}\n", stack.Count );

        for( int i = 0; i < array.Length; i++ )
            sb.AppendFormat( "\t{0}. {1}\n", i, array[i] );

        return sb.ToString();
    }

#endif
}
