namespace SportsStatistics.ArchitectureTests.Layers;

public class DomainLayerTests : BaseTest
{
    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_ApplicationLayer()
    {
        var t = ApplicationAssembly.GetName().Name;
        TestResult result = Types.InAssembly(DomainAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
