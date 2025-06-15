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
    public void ShouldReturnOneSecond_WhenWednesdayAndFridayAt12Hour(int day)
    {
        //Arrange
        _dateTimeProviderMock.Setup(x => x.Now).Returns(new DateTime(2025, 06, day, 12, 30, 45));
        
        //Act
        var result = _delayCalculator.CalculateDelay();
        
        //Assert
        Assert.That(result, Is.EqualTo(TimeSpan.FromSeconds(1)));
    }
    
    [TestCase(2, 2)]
    [TestCase(3, 1)]
    [TestCase(5, 1)]
    [TestCase(7, 4)]
    [TestCase(8, 3)]
    public void ShouldReturnDays_ForGivenDay(int day, int expectedDate)
    {
        //Arrange
        var date = new DateTime(2025, 06, day, 12, 15, 00, DateTimeKind.Local);
        _dateTimeProviderMock.Setup(x => x.Now).Returns(date);
        
        //Act
        var result = _delayCalculator.CalculateDelay();
        
        //Assert
        Assert.That(result, Is.EqualTo(TimeSpan.FromDays(expectedDate)));
    }
}