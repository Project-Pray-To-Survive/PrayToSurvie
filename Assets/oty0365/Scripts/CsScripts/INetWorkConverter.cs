using UnityEngine;

public interface INetWorkConverter
{
    public void Convert<T>(IData data, out T result);
}
