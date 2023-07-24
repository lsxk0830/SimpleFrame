using System;
namespace Blue
{
    public struct BindablePropertyUnSubscribe<T> : IUnSubscribe
    {
        private Action<T> _onPropertyChanged;
        private BindableProperty<T> _property;

        public BindablePropertyUnSubscribe(BindableProperty<T> property,Action<T> onPropertyChanged) 
        {
            _property = property;
            _onPropertyChanged = onPropertyChanged;
        }

        public void UnSubscribe()
        {
            _property.UnSubscribe(_onPropertyChanged);
            _property = null;
            _onPropertyChanged = null;
        }
    }
}
