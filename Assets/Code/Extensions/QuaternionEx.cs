using UnityEngine;

public static class QuaternionEx
{
    public static bool Approx2d( this Quaternion a, Quaternion b )
    {
        return a.eulerAngles.z.Approx( b.eulerAngles.z );
    }
}
