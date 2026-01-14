using System;

public class Stat<T> where T : IEquatable<T>
{
    public event Action<T> OnValueChange;
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            if (_value == null || !_value.Equals(value))
            {
                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }
    }
}