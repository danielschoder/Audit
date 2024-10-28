using Audit.Application.Commands;
using Audit.Application.Handlers.CommandHandlers;
using Audit.Infrastructure.Persistence;
using IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Audit.Application.Tests.CommandHandlers;

[TestFixture]
public class InsertDbContentChangeCommandHandlerTests
{
    private Mock<IConfiguration> _mockConfiguration;
    private ApplicationDbContext _context;
    private InsertDbContentChangeCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _mockConfiguration = new Mock<IConfiguration>();
        _context = new ApplicationDbContext(options, _mockConfiguration.Object);
        _handler = new InsertDbContentChangeCommandHandler(_context);
    }

    [Test]
    public async Task Handle_ShouldAddDbContentChange_WhenCommandIsHandled()
    {
        // Arrange
        var auditLogMessage = new AuditLogMessage
        {
            EntityId = "123",
            EntityName = "SampleEntity",
            FieldName = "FieldName",
            OldContent = "OldValue",
            NewContent = "NewValue",
            ChangedBy = "user@example.com",
            ChangedById = "user123"
        };

        var command = new InsertDbContentChangeCommand(auditLogMessage);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var dbContentChanges = await _context.DbContentChanges.ToListAsync();
        Assert.That(dbContentChanges, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(dbContentChanges[0].EntityId, Is.EqualTo(auditLogMessage.EntityId));
            Assert.That(dbContentChanges[0].EntityName, Is.EqualTo(auditLogMessage.EntityName));
            Assert.That(dbContentChanges[0].FieldName, Is.EqualTo(auditLogMessage.FieldName));
            Assert.That(dbContentChanges[0].OldContent, Is.EqualTo(auditLogMessage.OldContent));
            Assert.That(dbContentChanges[0].NewContent, Is.EqualTo(auditLogMessage.NewContent));
            Assert.That(dbContentChanges[0].ChangedBy, Is.EqualTo(auditLogMessage.ChangedBy));
            Assert.That(dbContentChanges[0].ChangedById, Is.EqualTo(auditLogMessage.ChangedById));
        });
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Database?.EnsureDeleted();
        _context?.Dispose();
    }
}
