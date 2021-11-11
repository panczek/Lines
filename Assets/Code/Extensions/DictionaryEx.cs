using System.Collections.Generic;

public static class DictionaryEx
{
    public static string PrintKeyValues<TKey, TVal>( this IDictionary<TKey, TVal> dict )
    {
        var builder = new System.Text.StringBuilder();

        builder.AppendFormat( "{0}<{1}, {2}>: {3} key value pairs\n", dict.GetType().Name, typeof( TKey ).Name, typeof( TVal ).Name, dict.Count );

        foreach( var kv in dict )
            builder.AppendFormat( "Key: {0}, Val: {1}\n", kv.Key, kv.Value );

        return builder.ToString();
    }

    public static TVal GetOrAddValue<TKey, TVal>(this IDictionary<TKey, TVal> dict, TKey key, System.Func<TVal> valueProvider)
    {
        TVal value = default;
        if( !dict.TryGetValue( key, out value ) ) 
        {
            value = valueProvider();
            dict.Add( key, value );
        }
        return value;
    }
}
