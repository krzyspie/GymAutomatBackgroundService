using GymAutomatBackgroundService.Api.HttpClients;
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
}