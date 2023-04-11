
using VismaInternshipTask.Repository;
using VismaInternshipTask.Repository.Entity;
using VismaInternshipTask.Service;
namespace VismaResourceShortage
{
    class Program
    {
        static void Main(string[] args)
        {
            var Path = "../../../test.json";
            var shortageRepository = new ShortageRepository(Path);
            var shortageService = new ShortageService(shortageRepository);
            var shortageManager = new ShortageManager(shortageService);
            shortageManager.Run();

        }
    }

    public class ShortageManager
    {
        private readonly IShortageService _service;
        private List<Shortage> _filteredShortages;
        public ShortageManager(IShortageService service)
        {
            _service = service;
            _filteredShortages = new List<Shortage>();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Visma's resource shortage manager!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Register a new shortage");
                Console.WriteLine("2. Delete a shortage");
                Console.WriteLine("3. List all shortages");
                Console.WriteLine("4. Filter shortages");
                Console.WriteLine("5. Exit");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterNewShortage();
                        break;
                    case "2":
                        DeleteShortage();
                        break;
                    case "3":
                        ListShortages();
                        break;
                    case "4":
                        FilterShortages();
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void RegisterNewShortage()
        {
            Console.WriteLine();
            Console.WriteLine("Please provide the following information:");
            Console.Write("Title: ");
            var title = Console.ReadLine().ToLower();
            Console.Write("Name: ");
            var name = Console.ReadLine().ToLower();

            string room;
            while (true)
            {
                Console.Write("Room (Meeting room / kitchen / bathroom): ");
                room = Console.ReadLine().ToLower();
                if (room == "meeting room" || room == "kitchen" || room == "bathroom")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid room. Please choose Meeting room, kitchen, or bathroom.");
                }
            }

            string category;
            while (true)
            {
                Console.Write("Category (Electronics / Food / Other): ");
                category = Console.ReadLine().ToLower();
                if (category == "electronics" || category == "food" || category == "other")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid category. Please choose Electronics, Food, or Other.");
                }
            }

            int priority;
            while (true)
            {
                Console.Write("Priority (1-10, 1 - not important, 10 - very important): ");
                var priorityInput = Console.ReadLine();
                if (int.TryParse(priorityInput, out priority) && priority >= 1 && priority <= 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid priority. Please enter an integer between 1 and 10.");
                }
            }

            _service.CreateShortage(title, name, room, category, priority);
        }

        private void DeleteShortage()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter the title of the shortage you want to delete:");
            var title = Console.ReadLine().ToLower();
            Console.WriteLine("Are you sure want to delete? (Type 'Yes' if u want to delete)");
            var confirmation = Console.ReadLine().ToLower();
            if (confirmation == "yes") {
                _service.DeleteShortage(title);
            }

        }
        private void ListShortages()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================================================================");
            Console.WriteLine("List of all shortages:");
            var shortages = _service.GetAllShortages();
            var count = shortages.Count();
         
            if (count == 0)
            {
                Console.WriteLine("No shortages found.");
            }
            else
            {
                foreach (var shortage in shortages)
                {
                    Console.WriteLine();
                    Console.WriteLine("Title: " + shortage.Title);
                    Console.WriteLine("Name: " + shortage.Name);
                    Console.WriteLine("Room: " + shortage.Room);
                    Console.WriteLine("Category: " + shortage.Category);
                    Console.WriteLine("Priority: " + shortage.Priority);
                    Console.WriteLine("Created On: " + shortage.CreatedOn);
                }
            }
            Console.WriteLine("============================================================================================================");
        }

        private void FilterShortages()
        {
            Console.WriteLine();
            Console.WriteLine("Please choose a filter option:");
            Console.WriteLine("1. Filter by title");
            Console.WriteLine("2. Filter by CreatedOn date");
            Console.WriteLine("3. Filter by Category");
            Console.WriteLine("4. Filter by Room");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter the title: ");
                    var title = Console.ReadLine();
                    _filteredShortages = _service.GetShortages(title: title).ToList();
                    break;
                case "2":
                    Console.WriteLine("Enter the CreatedOn from date (yyyy-MM-dd): ");
                    var createdOnFrom = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the CreatedOn to date (yyyy-MM-dd): ");
                    var createdTo = DateTime.Parse(Console.ReadLine());
                    _filteredShortages = _service.GetShortages(createdOnFrom: createdOnFrom,createdOnTo: createdTo).ToList();
                    break;
                case "3":
                    string category;
                    while (true)
                    {
                        Console.Write("Enter the category (Electronics / Food / Other): ");
                        category = Console.ReadLine().ToLower();
                        if (category == "electronics" || category == "food" || category == "other")
                        {
                            _filteredShortages = _service.GetShortages(category: category).ToList();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid category. Please choose Electronics, Food, or Other.");
                        }
                    }
                    break;
                case "4":
                    string room;
                    while (true)
                    {
                        Console.Write("Enter the room (Meeting room / kitchen / bathroom): ");
                        room = Console.ReadLine().ToLower();
                        if (room == "meeting room" || room == "kitchen" || room == "bathroom")
                        {
                            _filteredShortages = _service.GetShortages(room: room).ToList();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid room. Please choose Meeting room, kitchen, or bathroom.");
                        }
                    }

                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("============================================================================================================");
            Console.WriteLine("Filtered shortages:");
            if (_filteredShortages.Count == 0)
            {
                Console.WriteLine("No shortages found.");
            }
            else
            {
                foreach (var shortage in _filteredShortages)
                {
                    Console.WriteLine();
                    Console.WriteLine("Title: " + shortage.Title);
                    Console.WriteLine("Name: " + shortage.Name);
                    Console.WriteLine("Room: " + shortage.Room);
                    Console.WriteLine("Category: " + shortage.Category);
                    Console.WriteLine("Priority: " + shortage.Priority);
                    Console.WriteLine("Created On: " + shortage.CreatedOn);
                }
            }
            Console.WriteLine("============================================================================================================");
        }
    }
}

