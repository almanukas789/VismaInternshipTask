using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace VismaInternshipTask.Repository
{
    public class ShortageRepository : IShortageRepository
    {
        private readonly string _filePath = "../../../shortages.json";

        public ShortageRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Shortage> GetAllShortages()
        {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Shortage>>(json);
        }

        public Shortage GetShortageById(int id)
        {
            var shortages = GetAllShortages();
            return shortages.FirstOrDefault(s => s.Id == id);
        }

        public List<Shortage> GetShortagesByTitle(string title)
        {
            var shortages = GetAllShortages();
            return shortages.Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Shortage> GetShortagesByCategory(string category)
        {
            if (!Enum.TryParse(category, true, out Category categoryEnum))
            {
                return new List<Shortage>();
            }

            var shortages = GetAllShortages();
            return shortages.Where(s => s.Category == categoryEnum).ToList();
        }

        public List<Shortage> GetShortagesByRoom(string room)
        {
            if (!Enum.TryParse(room, true, out Room roomEnum))
            {
                return new List<Shortage>();
            }

            var shortages = GetAllShortages();
            return shortages.Where(s => s.Room == roomEnum).ToList();
        }

        public List<Shortage> GetShortagesByCreatedOn(DateTime startDate, DateTime endDate)
        {
            var shortages = GetAllShortages();
            return shortages.Where(s => s.CreatedOn >= startDate && s.CreatedOn <= endDate).ToList();
        }

        public void AddShortage(Shortage shortage)
        {
            var shortages = GetAllShortages();

            if (shortages.Any(s => s.Title == shortage.Title && s.Room == shortage.Room))
            {
                Console.WriteLine($"Shortage with title '{shortage.Title}' and room '{shortage.Room}' already exists.");
            }
            else
            {
                shortage.Id = shortages.Count > 0 ? shortages.Max(s => s.Id) + 1 : 1;
                shortage.CreatedOn = DateTime.Now;
                shortages.Add(shortage);
                var json = JsonConvert.SerializeObject(shortages);
                File.WriteAllText(_filePath, json);
            }
        }

        public void UpdateShortage(int id, Shortage shortage)
        {
            var shortages = GetAllShortages();

            var index = shortages.FindIndex(s => s.Id == id);

            if (index >= 0)
            {
                var existingShortage = shortages[index];

                if (existingShortage.Title != shortage.Title || existingShortage.Room != shortage.Room)
                {
                    if (shortages.Any(s => s.Title == shortage.Title && s.Room == shortage.Room))
                    {
                        Console.WriteLine($"Shortage with title '{shortage.Title}' and room '{shortage.Room}' already exists.");
                        return;
                    }
                }

                existingShortage.Title = shortage.Title;
                existingShortage.Name = shortage.Name;
                existingShortage.Room = shortage.Room;
                existingShortage.Category = shortage.Category;
                existingShortage.Priority = shortage.Priority;
                var json = JsonConvert.SerializeObject(shortages);
                File.WriteAllText(_filePath, json);
            }
            else
            {
                Console.WriteLine($"Shortage with id '{id}' not found.");
            }
        }

        public void DeleteShortage(int id)
        {
            var shortages = GetAllShortages();

            var index = shortages.FindIndex(s => s.Id == id);

            if (index >= 0)
            {
                shortages.RemoveAt(index);

                var json = JsonConvert.SerializeObject(shortages);
                File.WriteAllText(_filePath, json);
            }
            else
            {
                Console.WriteLine($"Shortage with id '{id}' not found.");
            }
        }


    }
}
