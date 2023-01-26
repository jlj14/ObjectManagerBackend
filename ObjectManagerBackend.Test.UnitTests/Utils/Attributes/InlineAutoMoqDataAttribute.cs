using ObjectManagerBackend.Test.UnitTests.Utils.Customizations;

namespace ObjectManagerBackend.Test.UnitTests.Utils.Attributes
{
    /// <summary>
    /// Automoq inline data attribute
    /// </summary>
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base(new AutoMoqDataAttribute(), values)
        {
        }
    }
}
