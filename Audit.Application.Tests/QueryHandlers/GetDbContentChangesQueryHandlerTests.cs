using Audit.Application.Handlers.QueryHandlers;
using Audit.Application.Queries;
using Audit.Domain.Entities;
using Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Audit.Application.Tests.QueryHandlers
{
    [TestFixture]
    public class GetDbContentChangesQueryHandlerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private ApplicationDbContext _context;
        private GetDbContentChangesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockConfiguration = new Mock<IConfiguration>();
            _context = new ApplicationDbContext(options, _mockConfiguration.Object);
            _handler = new GetDbContentChangesQueryHandler(_context);
        }

        [Test]
        public async Task Handle_ShouldReturnPaginatedResult_WhenDbContentChangesExist()
        {
            // Arrange
            var contentChanges = new List<DbContentChange>
            {
                new()
                {
                    EntityId = "1",
                    EntityName = "SampleEntity1",
                    FieldName = "FieldName1",
                    OldContent = "OldValue1",
                    NewContent = "NewValue1",
                    ChangedBy = "user1@example.com",
                    ChangedById = "user123",
                    ChangedDateTime = DateTime.UtcNow.AddDays(-1)
                },
                new()
                {
                    EntityId = "2",
                    EntityName = "SampleEntity2",
                    FieldName = "FieldName2",
                    OldContent = "OldValue2",
                    NewContent = "NewValue2",
                    ChangedBy = "user2@example.com",
                    ChangedById = "user456",
                    ChangedDateTime = DateTime.UtcNow
                }
            };

            await _context.DbContentChanges.AddRangeAsync(contentChanges);
            await _context.SaveChangesAsync();

            var query = new GetDbContentChangesQuery(PageNumber: 1, PageSize: 1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            var data = result.Data.ToList();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(data, Has.Count.EqualTo(1));
                Assert.That(result.CurrentPage, Is.EqualTo(1));
                Assert.That(result.PageSize, Is.EqualTo(1));
                Assert.That(result.TotalRecords, Is.EqualTo(2));
            });
            Assert.Multiple(() =>
            {
                Assert.That(data[0].EntityId, Is.EqualTo("2"));
                Assert.That(data[0].EntityName, Is.EqualTo("SampleEntity2"));
                Assert.That(data[0].OldContent, Is.EqualTo("OldValue2"));
                Assert.That(data[0].NewContent, Is.EqualTo("NewValue2"));
                Assert.That(data[0].ChangedBy, Is.EqualTo("user2@example.com"));
            });
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Database?.EnsureDeleted();
            _context?.Dispose();
        }
    }
}
