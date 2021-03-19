using AppResultRWOP.Utilities;

namespace Tests.ValueObject
{
    class ValueObjectMockInt : ValueObject<ValueObjectMockInt>
    {
        public int Value { get; private set; }

        private ValueObjectMockInt() { }

        protected override bool ComparePropertiesForEquality(ValueObjectMockInt obj) => Value == obj.Value;

        protected override string DefineToStringFormat() => Value.ToString();

        public static ValueObjectMockInt Make(int value) => new ValueObjectMockInt { Value = value };
    }
}
