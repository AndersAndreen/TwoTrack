namespace TwoTrack.Core.UnitTests.ValueObject
{
    class ValueObjectStringMock : ValueObject<ValueObjectStringMock>
    {
        public string Value { get; private set; }

        private ValueObjectStringMock() { }

        protected override bool ComparePropertiesForEquality(ValueObjectStringMock obj) => Value == obj.Value;

        protected override string DefineToStringFormat() => Value.ToString();

        public static ValueObjectStringMock Make(string value)
        {
            NullCheck(value, "ValueObjectStringMock Make(string value)");
            return new ValueObjectStringMock { Value = value };
        }
    }
}
