using MediatR;
using Moq;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class MediatorMock
    {
        public static Mock<IMediator> GetMediator()
        {
            var mediatorMock = new Mock<IMediator>();

            mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<Unit>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            return mediatorMock;
        }
    }
}
