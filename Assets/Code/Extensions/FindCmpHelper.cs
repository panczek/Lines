using System.Collections.Generic;
using UnityEngine;

public static class FindCmpHelper<T>
    where T : Component
{
    private readonly static List<T> temp = new List<T>();

    public static T GetInParent( GameObject gob, bool includeInactive )
    {
        gob.GetComponentsInParent( includeInactive, temp );

        T result = temp[0];
        temp.Clear();

        return result;
    }
}
