using Audit.Application.Handlers.CommandHandlers;
using Audit.Infrastructure.Consumers;
using IntegrationEvents;
using MassTransit;
using MediatR;
using Moq;

namespace Audit.Infrastructure.Tests.Consumers
{
    [TestFixture]
    public class AuditLogConsumerTests
    {
        private Mock<IMediator> _mediatorMock;
        private AuditLogConsumer _consumer;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _consumer = new AuditLogConsumer(_mediatorMock.Object);
        }

        [Test]
        public async Task Consume_ShouldSendInsertDbContentChangeCommand_WhenMessageIsConsumed()
        {
            // Arrange
            var auditLogMessage = new AuditLogMessage();
            var context = Mock.Of<ConsumeContext<AuditLogMessage>>(c => c.Message == auditLogMessage);

            // Act
            await _consumer.Consume(context);

            // Assert
            _mediatorMock.Verify(m => m.Send(It.Is<InsertDbContentChange.Command>(
                cmd => cmd.AuditLogMessage == auditLogMessage),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
