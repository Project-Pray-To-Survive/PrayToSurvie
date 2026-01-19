using UnityEngine;

[System.Serializable] 
public class MinMaxRange<T> where T : struct 
{
    public T min;
    public T max;
    
    public MinMaxRange(T min, T max)
    {
        this.min = min;
        this.max = max;
    }
    
    public float Rand()
    {
        if (typeof(T) == typeof(int))
        {
            return Random.Range(System.Convert.ToInt32(min), System.Convert.ToInt32(max));
        }
        return Random.Range(System.Convert.ToSingle(min), System.Convert.ToSingle(max));
    }
}