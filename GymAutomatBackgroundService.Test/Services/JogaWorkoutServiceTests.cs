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
        var result = _service.GetJogaWorkoutToRegister(null);
        Assert.IsNull(result);
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsNull_WhenListIsEmpty()
    {
        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel>());
        Assert.IsNull(result);
    }

    [Test]
    public void GetJogaWorkoutToRegister_SkipsSundayClasses()
    {
        var sundayClass = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 13), // Sunday
            ParticipantsNumber = 0,
            ParticipantsLimit = 10
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 10));

        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { sundayClass });

        Assert.IsNull(result);
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsWorkout_WhenRegistrationNotOpenYet()
    {
        var workout = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 15),
            ParticipantsNumber = 0,
            ParticipantsLimit = 0
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 10));

        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { workout });

        Assert.AreEqual(workout, result);
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsWorkout_WhenThereAreFreeSlots()
    {
        var workout = new JogaWorkoutModel
        {
            StartDate = new DateTime(2025, 7, 11),
            ParticipantsNumber = 5,
            ParticipantsLimit = 10
        };

        _dateTimeProviderMock.Setup(p => p.Now).Returns(new DateTime(2025, 7, 11));

        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { workout });

        Assert.AreEqual(workout, result);
    }

    [Test]
    public void GetJogaWorkoutToRegister_ReturnsFirstEligibleWorkout_InCorrectOrder()
    {
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

        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { lateFreeClass, earlyClass });

        Assert.AreEqual(earlyClass, result);
    }
    
    [Test]
    public void GetJogaWorkoutToRegister_ReturnsEligibleWorkoutWithFreeClass_InCorrectOrder()
    {
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

        var result = _service.GetJogaWorkoutToRegister(new List<JogaWorkoutModel> { lateFreeClass, earlyFullClass });

        Assert.AreEqual(lateFreeClass, result);
    }
}