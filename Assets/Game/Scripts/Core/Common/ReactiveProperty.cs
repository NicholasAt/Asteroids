using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Core.Common
{
    public class ReactiveProperty<T>
    {
        private T _value;
        public event Action<T> Changed;

        public ReactiveProperty() { }
        public ReactiveProperty(T initial) { _value = initial; }

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;
                _value = value;
                Changed?.Invoke(value);
            }
        }
    }
}
