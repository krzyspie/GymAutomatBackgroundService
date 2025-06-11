using GymAutomatBackgroundService.Api.Services;
using GymAutomatBackgroundService.Api.Utilities;
using Moq;

namespace GymAutomatBackgroundService.Test.Services;

public class DelayCalculatorTests
{
    private Mock<IDateTimeProvider> _dateTimeProviderMock;
    private DelayCalculator _delayCalculator;
    
    [SetUp]
    public void Setup()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _delayCalculator = new DelayCalculator(_dateTimeProviderMock.Object);
    }

    [TestCase(11)]
    [TestCase(13)]
    public void ShouldReturnOneSecond_WhenWendsdayAndFriday(int day)
    {
        //Arrange
        _dateTimeProviderMock.Setup(x => x.Now).Returns(new DateTime(2025, 06, day, 12, 30, 45));
        
        //Act
        var result = _delayCalculator.CalculateDelay();
        
        //Assert
        Assert.That(result, Is.EqualTo(1000));
    }
}