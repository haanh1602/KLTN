using UnityEngine;
using Random = UnityEngine.Random;

public class MyRange
{
    public float Start { get; }
    public float End { get; }

    public MyRange(int start, int end)
    {
        Start = start;
        End = end;
    }
    
    public MyRange(float start, float end)
    {
        Start = start;
        End = end;
    }

    public bool IsInRange(float value, bool includeBounds = true)
    {
        if (includeBounds) return value >= Start && value <= End;
        return value > Start && value < End;
    }

    public bool IsInRange(MyRange range, bool includeBounds = true)
    {
        if (includeBounds) return range.Start >= Start && range.End <= End;
        return range.Start > Start && range.End < End;
    }
    
    public float RandomInRange()
    {
        return Random.Range(Start, End);
    }

    public int RandomIntInRange(bool includeEnd)
    {
        return Random.Range((int) Start, (int) End + (includeEnd ? 1 : 0));
    }

    public Vector2 RandomVector2InRange()
    {
        return new Vector2(RandomInRange(), RandomInRange());
    }

    public Vector2Int RandomVector2IntInRange(bool includeEnd)
    {
        return new Vector2Int(RandomIntInRange(includeEnd), RandomIntInRange(includeEnd));
    }

    public Vector3 RandomVector3InRange()
    {
        return new Vector3(RandomInRange(), RandomInRange(), RandomInRange());
    }

    public Vector3Int RandomVector3IntInRange(bool includeEnd)
    {
        return new Vector3Int(RandomIntInRange(includeEnd), RandomIntInRange(includeEnd), RandomIntInRange(includeEnd));
    }
    
    public static float RandomInRangeExclude(MyRange range, MyRange excludeRange)
    {
        if (range.Start > excludeRange.Start || range.End < excludeRange.End)
        {
            Debug.LogWarning("MyRangeFloat out of around range");
            return 0;
        }
        
        return Random.Range(0, 2) == 0? Random.Range(range.Start, excludeRange.Start) : Random.Range(excludeRange.End, range.End);
    }

    public static float RandomOutRangeIn(MyRange range, MyRange aroundRange)
    {
        return RandomInRangeExclude(aroundRange, range);
    }
}



