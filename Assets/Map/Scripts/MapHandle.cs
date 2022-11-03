using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandle 
{
    public static Vector2 seed;

    
    public static float Random(Vector2 inVec)
    {

        //float value = Mathf.Sin(Vector2.Dot(inVec, new Vector2(12.9898f, 78.233f)))* 43758.5453123f;
        float value = Mathf.Sin(Vector2.Dot(inVec, new Vector2(seed.x,seed.y))) * 43758.5453123f;
        value -= Mathf.Floor(value);
        return value;
    }

    public static float Noise(Vector2 inVec)
    {
        Vector2 floor = new Vector2(Mathf.Floor(inVec.x), Mathf.Floor(inVec.y));
        Vector2 fract = inVec - floor;

        float a = Random(floor);
        float b = Random(floor + new Vector2(1, 0));
        float c = Random(floor + new Vector2(0, 1));
        float d = Random(floor + new Vector2(1, 1));

        Vector2 u = fract * fract * (Vector2.one*3 - 2.0f* fract);
        return Mathf.Lerp(a, b, u.x) + (c - a) * u.y * (1f - u.x) + (d - b) * u.x * u.y; ;
    }
}
