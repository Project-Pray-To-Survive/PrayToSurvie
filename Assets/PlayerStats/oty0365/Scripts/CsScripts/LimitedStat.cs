using System;
using System.Collections.Generic;

public class LimitedStat<T> where T : IEquatable<T>, IComparable<T>
{
    public event Action<T, T> OnValueChange;

    private T _value;
    private T _maxValue;
    private T _minValue;

    public LimitedStat(T initialValue, T max)
    {
        _maxValue = max;
        _value = initialValue;
    }

    public T Value
    {
        get => _value;
        set
        {
            T nextValue = value;
            
            if (nextValue.CompareTo(_maxValue) > 0)
            {
                nextValue = _maxValue;
            }

            if (nextValue.CompareTo(_minValue) < 0)
            {
                nextValue = _minValue;
            }

            if (EqualityComparer<T>.Default.Equals(_value, nextValue)) return;
            _value = nextValue;
            OnValueChange?.Invoke(_value, _maxValue);
        }
    }

    public T MaxValue
    {
        get => _maxValue;
        set
        {
            if (EqualityComparer<T>.Default.Equals(_maxValue, value)) return;
            _maxValue = value;
                
            if (_value.CompareTo(_maxValue) > 0)
            {
                _value = _maxValue;
            }

            OnValueChange?.Invoke(_value, _maxValue);
        }
    }

    public T MinValue
    {
        get => _minValue;
        set
        {
            if (EqualityComparer<T>.Default.Equals(_minValue, value)) return;
            _minValue = value;
            if (_value.CompareTo(_minValue) < 0)
            {
                _value = _minValue;
            }
            OnValueChange?.Invoke(_value, _minValue);
        }
    }
}