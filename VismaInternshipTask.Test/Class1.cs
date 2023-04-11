using NUnit.Framework;

namespace VismaInternshipTask.Test
{
    [TestFixture]
    public class ShortageRepositoryTests
    {
        [Test]
        public void Create_Shortage_Should_Return_New_Shortage_Object()
        {
            // Arrange
            var repo = new ShortageRepository("path/to/shortages.json");
            var newShortage = new Shortage
            {
                Title = "New Shortage",
                Room = "Meeting Room",
                Category = "Other",
                Priority = 5
            };

            // Act
            var result = repo.Create(newShortage);

            // Assert
            Assert.IsInstanceOf<Shortage>(result);
        }
    }
}