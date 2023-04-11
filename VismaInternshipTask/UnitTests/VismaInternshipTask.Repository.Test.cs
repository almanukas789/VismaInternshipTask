
using VismaInternshipTask.Repository.Entity;
using VismaInternshipTask.Repository;
using NUnit.Framework;

namespace VismaInternshipTask.UnitTests
{
    public class ShortageRepositoryTests
    {
        private ShortageRepository _shortageRepository;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _shortageRepository = new ShortageRepository("test.json");
        }

        [Test]
        public void TestCreate()
        {
            // Arrange
            var shortage = new Shortage
            {
                Title = "Test Shortage",
                Name = "John Doe",
                Room = "Kitchen",
                Category = "Food",
                Priority = 5
            };

            // Act
            var createdShortage = _shortageRepository.Create(shortage);

            // Assert
            Assert.AreEqual(shortage.Title, createdShortage.Title);
            Assert.AreEqual(shortage.Name, createdShortage.Name);
            Assert.AreEqual(shortage.Room, createdShortage.Room);
            Assert.AreEqual(shortage.Category, createdShortage.Category);
            Assert.AreEqual(shortage.Priority, createdShortage.Priority);
        }

        [Test]
        public void TestGetAllShortages()
        {
            // Arrange

            // Act
            var shortages = _shortageRepository.GetAllShortages().ToList();

            // Assert
            Assert.IsNotNull(shortages);
            Assert.IsTrue(shortages.Any());
        }

        [Test]
        public void TestGetShortagesByTitle()
        {
            // Arrange
            var title = "Test Shortage";

            // Act
            var shortages = _shortageRepository.GetShortages(title: title).ToList();

            // Assert
            Assert.IsNotNull(shortages);
            Assert.IsTrue(shortages.Any());
            Assert.IsTrue(shortages.All(s => s.Title.ToLower().Contains(title.ToLower())));
        }

        [Test]
        public void TestGetShortagesByRoom()
        {
            // Arrange
            var room = "Kitchen";

            // Act
            var shortages = _shortageRepository.GetShortages(room: room).ToList();

            // Assert
            Assert.IsNotNull(shortages);
            Assert.IsTrue(shortages.Any());
            Assert.IsTrue(shortages.All(s => s.Room.ToLower() == room.ToLower()));
        }

        [Test]
        public void TestDelete()
        {
            // Arrange
            var title = "Test Shortage";
            var shortage = new Shortage
            {
                Title = title,
                Name = "John Doe",
                Room = "Kitchen",
                Category = "Food",
                Priority = 5
            };
            _shortageRepository.Create(shortage);

            // Act
            _shortageRepository.Delete(title);
            var deletedShortage = _shortageRepository.GetAllShortages().FirstOrDefault(s => s.Title == title);

            // Assert
            Assert.IsNull(deletedShortage);
        }
    }
}
