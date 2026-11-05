using System;
using System.Collections.Generic;

namespace Assets.Scripts.Gameplay.Common
{
    public class ObjectsPool<T> where T : class
    {
        private readonly Stack<T> _free = new Stack<T>();
        private readonly Func<T> _create;

        public ObjectsPool(Func<T> create)
        {
            _create = create;
        }

        public T Pop()
        {
            if (_free.Count > 0)
            {
                return _free.Pop();
            }
            return _create();
        }
        public void Return(T value)
        {
            _free.Push(value);
        }
    }
}