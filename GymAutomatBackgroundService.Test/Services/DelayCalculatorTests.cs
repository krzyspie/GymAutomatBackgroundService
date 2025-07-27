using GymAutomatBackgroundService.Api.Services;
using GymAutomatBackgroundService.Api.Utilities;
using Moq;

namespace GymAutomatBackgroundService.Test.Services;

[TestFixture]
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
    
    [Test]
    public void ShouldReturnDays_ForGivenDay()
    {
        //Arrange
        var date = new DateTime(2025, 06, 10, 12, 20, 00, DateTimeKind.Local);
        var workoutDate = new DateTime(2025, 06, 15, 12, 20, 00, DateTimeKind.Local);
        _dateTimeProviderMock.Setup(x => x.Now).Returns(date);
        
        //Act
        var result = _delayCalculator.CalculateDelay(workoutDate);
        
        //Assert
        Assert.That(result, Is.EqualTo(TimeSpan.FromDays(2)));
    }
    
    [Test]
    public void ShouldReturnSeconds_WhenWorkoutDateIsEarly()
    {
        //Arrange
        var date = new DateTime(2025, 06, 10, 12, 20, 00, DateTimeKind.Local);
        var workoutDate = new DateTime(2025, 06, 11, 12, 20, 00, DateTimeKind.Local);
        _dateTimeProviderMock.Setup(x => x.Now).Returns(date);
        
        //Act
        var result = _delayCalculator.CalculateDelay(workoutDate);
        
        //Assert
        Assert.That(result, Is.EqualTo(TimeSpan.FromSeconds(10)));
    }
}