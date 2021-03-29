using TwoTrack.Core;

namespace TwoTrack.UnitTests.ValueObject
{
    class ValueObjectIntMock : ValueObject<ValueObjectIntMock>
    {
        public int Value { get; private set; }

        private ValueObjectIntMock() { }

        protected override bool ComparePropertiesForEquality(ValueObjectIntMock obj) => Value == obj.Value;

        protected override string DefineToStringFormat() => Value.ToString();

        public static ValueObjectIntMock Make(int value) => new ValueObjectIntMock { Value = value };
    }
}
