namespace SportsStatistics.ArchitectureTests.Layers;

public class InfrastructureLayerTests : BaseTest
{
    [Fact]
    public void InfrastructureLayer_Should_NotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(InfrastructureAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
