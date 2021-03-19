namespace AppResultRWOP.Utilities
{
    public abstract class ValueObject<T>
    {
        protected abstract bool ComparePropertiesForEquality(T obj);
        protected abstract string DefineToStringFormat();

        public override string ToString() => DefineToStringFormat();
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
            => obj?.GetType() == typeof(T) && ComparePropertiesForEquality((T)obj);
        public static bool operator ==(ValueObject<T> x, ValueObject<T> y) => x?.Equals(y) ?? false;
        public static bool operator !=(ValueObject<T> x, ValueObject<T> y) => (!x?.Equals(y)) ?? false;
    }
}
