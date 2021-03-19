using AppResultRWOP.Utilities;

namespace Tests.ValueObject
{
    class ValueObjectMock : ValueObject<ValueObjectMock>
    {
        public string Value { get; private set; }

        private ValueObjectMock() { }

        protected override bool ComparePropertiesForEquality(ValueObjectMock obj) => Value == obj.Value;

        protected override string DefineToStringFormat() => Value.ToString();

        public static ValueObjectMock Make(string value) => new ValueObjectMock { Value = value };
    }
}
