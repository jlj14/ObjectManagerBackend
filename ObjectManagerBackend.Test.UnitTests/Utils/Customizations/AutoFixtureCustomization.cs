using AutoFixture.AutoMoq;

namespace ObjectManagerBackend.Test.UnitTests.Utils.Customizations
{
    /// <summary>
    /// Autofixture customization
    /// </summary>
    public class AutoFixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize(new ControllerCustomization());
        }
    }
}
