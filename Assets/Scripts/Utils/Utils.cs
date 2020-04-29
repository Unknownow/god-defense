using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float DistanceInXZ(Vector3 vec1, Vector3 vec2)
    {
        return Vector2.Distance(new Vector2(vec1.x, vec1.z), new Vector2(vec2.x, vec2.z));
    }
}
