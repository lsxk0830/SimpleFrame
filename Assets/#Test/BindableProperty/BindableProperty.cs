using System;

public class BindableProperty<T>
{
    private T mValue;
    public T Value
    {
        get => mValue;
        set
        {
            if (value.Equals(mValue)) return;
            mValue = value;
            mOnValueChanged?.Invoke(value);
        }
    }

    public Action<T> mOnValueChanged = mValue => { };

    public BindableProperty(T defaultValue = default)
    {
        mValue = defaultValue;
    }
}