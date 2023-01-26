using ObjectManagerBackend.Test.UnitTests.Utils.Customizations;

namespace ObjectManagerBackend.Test.UnitTests.Utils.Attributes
{
    /// <summary>
    /// Automoq data attribute
    /// </summary>
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture().Customize(new AutoFixtureCustomization()))
        {
        }
    }
}
