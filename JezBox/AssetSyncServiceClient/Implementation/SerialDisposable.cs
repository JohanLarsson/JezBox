using System;

namespace JezBox
{
    internal sealed class SerialDisposable<T> : IDisposable
        where T : IDisposable
    {
        private readonly object _gate = new object();
        private T _value;
        private bool _disposed;

        public SerialDisposable(T value)
        {
            _value = value;
        }

        public T Getvalue()
        {
            lock (_gate)
            {
                return _value;
            }
        }

        public void SetValue(T value)
        {
            lock (_gate)
            {
                _value?.Dispose();
                _value = value;
                if (_disposed)
                {
                    _value.Dispose();
                }
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _value?.Dispose();
        }
    }
}