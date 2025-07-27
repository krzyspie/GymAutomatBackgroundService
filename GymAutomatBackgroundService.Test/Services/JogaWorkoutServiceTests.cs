using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Services;
using GymAutomatBackgroundService.Api.Utilities;
using Moq;

namespace GymAutomatBackgroundService.Test.Services;

[TestFixture]
public class JogaWorkoutServiceTests
{
    private Mock<IDateTimeProvider> _dateTimeProviderMock;
    private JogaWorkoutService _service;
    
    [SetUp]
    public void SetUp()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _service = new JogaWorkoutService(_dateTimeProviderMock.Object);
    }
    
    [Test]
    public void GetJogaWorkoutToRegister_ReturnsNull_WhenListIsNull()
    {
        //Act
        var result = _service.GetJogaWorkoutToRegister(null);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.False);
            Assert.That(result.JogaWorkout, Is.Null);
        });
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsNull_WhenListIsEmpty()
    {
        ////Act
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel>());
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.False);
            Assert.That(result.JogaWorkout, Is.Null);
        });
    }

    [Test]
    public void GetJogaWorkoutToRegister_SkipsSundayClasses()
    {
        //Arrange
        var sundayClass = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 13), // Sunday
            ParticipantsNumber = 0,
            ParticipantsLimit = 10
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 10));

        //Act
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { sundayClass });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.False);
            Assert.That(result.JogaWorkout, Is.Null);
        });
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsWorkout_WhenRegistrationNotOpenYet()
    {
        //Arrange
        var workout = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 15),
            ParticipantsNumber = 0,
            ParticipantsLimit = 0
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 10));

        //Act
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { workout });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.True);
            Assert.That(result.JogaWorkout, Is.EqualTo(workout));
        });
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsWorkout_WhenThereAreFreeSlots()
    {
        //Arrange
        var workout = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 11),
            ParticipantsNumber = 5,
            ParticipantsLimit = 10
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 11));

        //Act
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { workout });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.True);
            Assert.That(result.JogaWorkout, Is.EqualTo(workout));
        });
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsFirstEligibleWorkout_InCorrectOrder()
    {
        //Arrange
        var earlyClass = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 11),
            ParticipantsNumber = 9,
            ParticipantsLimit = 10
        };

        var lateFreeClass = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 12),
            ParticipantsNumber = 5,
            ParticipantsLimit = 10
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 11));

        //Act
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { lateFreeClass, earlyClass });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.True);
            Assert.That(result.JogaWorkout, Is.EqualTo(earlyClass));
        });
    }
    
    [Test]
    public void GetJogaWorkoutToRegister_ReturnsEligibleWorkoutWithFreeClass_InCorrectOrder()
    {
        //Arrange
        var earlyFullClass = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 11),
            ParticipantsNumber = 10,
            ParticipantsLimit = 10
        };

        var lateFreeClass = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 12),
            ParticipantsNumber = 5,
            ParticipantsLimit = 10
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 11));

        //Act
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { lateFreeClass, earlyFullClass });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.CanRegister, Is.True);
            Assert.That(result.JogaWorkout, Is.EqualTo(lateFreeClass));
        });
    }
}