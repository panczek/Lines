using K2P.Core;
using UnityEngine;

using SysRandom = System.Random;
using UniRandom = UnityEngine.Random;

public static class RandomEx
{
    public static bool GetBool()
    {
        return UnityEngine.Random.Range( 0, 10 ) < 5;
    }

    public static float NextFloat( this SysRandom rng )
    {
        return (float)rng.NextDouble();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="min">inclusive</param>
    /// <param name="max">exclusive</param>
    /// <returns></returns>
    public static float NextFloat( this SysRandom rng, float min, float max )
    {
        return (float)( rng.NextDouble() * ( max - min ) + min );
    }

    public static int NextOtherThan( this SysRandom rng, int value, int min, int max )
    {
        if( min > max )
        {
            var tmp = min;
            min = max;
            max = tmp;
        }

        if( min == max || min + 1 == max ) return min;

        int curr = rng.Next( min, max );
        while( curr == value ) curr = rng.Next( min, max );
        return curr;
    }

    public static int RandomSeed()
    {
        return UniRandom.Range( int.MinValue, int.MaxValue );
    }


    public static Vector3 OnUnitCircle( float rangeMin, float rangeMax, float y = 0f )
    {
        var rotation = Quaternion.AngleAxis( UniRandom.Range( 0f, 360f ), Vector3.up );
        var range = UniRandom.Range( rangeMin, rangeMax );

        return ( ( rotation * Vector3.forward ) * range ).WithY( y );
    }
}
