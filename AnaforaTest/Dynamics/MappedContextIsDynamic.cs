using AnaforaTest.Dynamics.Fixtures;

namespace AnaforaTest.Dynamics
{
    [Collection("SeedingSensitive")]
    public class MappedContextIsDynamic : IClassFixture<MappedContextFixture>
    {
        public MappedContextIsDynamic(MappedContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GlobalMappedIsDynamicAsync()
        {
            using var ctx = await _fixture.GetGlobalDynamicContextAsync();
            using var mapCtx = await _fixture.GetGlobalMappedContextAsync();
            var prop = ctx.Properties.FirstOrDefault();
            Assert.NotNull(prop);
            var map = ctx.Properties.FirstOrDefault(i => i.Id == prop.Id);
            Assert.NotNull(map);
            Assert.Equal(prop.Name, map.Name);
        }

        private MappedContextFixture _fixture;
    }
}