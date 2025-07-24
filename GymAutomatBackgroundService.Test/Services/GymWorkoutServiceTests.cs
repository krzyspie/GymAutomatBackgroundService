using GymAutomatBackgroundService.Api.HttpClients;
using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Services;
using Moq;

namespace GymAutomatBackgroundService.Test.Services;

[TestFixture]
public class GymWorkoutServiceTests
{
    private Mock<IGymClient> _gymClientMock;
    private Mock<IRequestDataFactory> _requestDataFactoryMock;
    
    private GymWorkoutService _service;
    
    [SetUp]
    public void SetUp()
    {
        _gymClientMock = new Mock<IGymClient>();
        _requestDataFactoryMock = new Mock<IRequestDataFactory>();
        _service = new GymWorkoutService(_gymClientMock.Object, _requestDataFactoryMock.Object);
    }
    [Test]
    public async Task GetJogaWorkouts_ReturnsOnlyJogaWorkouts()
    {
        // Arrange
        var requestContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());

        _requestDataFactoryMock
            .Setup(f => f.GetJogaWorkoutsRequest(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(requestContent);

        var mockWorkouts = new List<WorkoutResult>
        {
            new()
            {
                ClassId = 1,
                Name = "Just Joga",
                StartDate = "2025-07-11T10:00:00",
                ParticipantsLimit = 15,
                ParticipantsNumber = 10
            },
            new()
            {
                ClassId = 2,
                Name = "Body Pump",
                StartDate = "2025-07-11T12:00:00",
                ParticipantsLimit = 20,
                ParticipantsNumber = 18
            },
            new()
            {
                ClassId = 3,
                Name = "JOGA FLOW",
                StartDate = "2025-07-12T08:00:00",
                ParticipantsLimit = 10,
                ParticipantsNumber = 5
            }
        };

        _gymClientMock
            .Setup(c => c.GetWorkouts(It.IsAny<FormUrlEncodedContent>()))
            .ReturnsAsync(new GymWorkoutsResponse { Results = mockWorkouts });

        // Act
        var result = await _service.GetJogaWorkouts();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].WorkoutId, Is.EqualTo(1));
        Assert.That(result[1].WorkoutId, Is.EqualTo(3));
    }
}