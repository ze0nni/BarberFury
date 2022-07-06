using System;

namespace Common {
        public readonly struct Identity<T>: IEquatable<Identity<T>>
        {
                public static Identity<T> Null => new Identity<T>();

                readonly int _value;

                public Identity(int value) {
                        _value = value;
                }

                public bool IsNull => _value == 0;

                public bool Equals(Identity<T> other) {
                        return _value == other._value;
                }

                public static bool operator==(Identity<T> l, Identity<T> r) {
                        return l._value == r._value;
                }

                public static bool operator!=(Identity<T> l, Identity<T> r) {
                        return l._value != r._value;
                }

                public override string ToString()
                {
                        return $"{typeof(T).Name}<{_value}>";
                }
        }
}