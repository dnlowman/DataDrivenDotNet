using Xunit;

namespace DataDrivenDotNet.Test
{
    public sealed class ExampleRepositoryTests
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(1, 2);
        }
    }
}
