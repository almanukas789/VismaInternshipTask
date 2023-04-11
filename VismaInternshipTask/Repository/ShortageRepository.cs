using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaInternshipTask.Repository.Entity;

namespace VismaInternshipTask.Repository
{
    public class ShortageRepository : IShortageRepository
    {
        private readonly string _filePath;

        public ShortageRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                using (var fileStream = File.CreateText(_filePath))
                {
                    fileStream.Write("[]");
                }
            }
        }

        public Shortage Create(Shortage shortage)
        {
            var shortages = GetAllShortages().ToList();
            var existingShortage = shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);
            if (existingShortage != null)
            {
                if (existingShortage.Priority < shortage.Priority)
                {
                    existingShortage.Priority = shortage.Priority;
                    existingShortage.CreatedOn = DateTime.UtcNow;
                    Console.WriteLine("Shortage registered successfully.");
                    SaveAllShortages(shortages);
                }else
                {
                    Console.WriteLine("This kind of shortage already exist!");
                }
                return existingShortage;
            }

            shortage.CreatedOn = DateTime.UtcNow;
            shortages.Add(shortage);
            SaveAllShortages(shortages);
            return shortage;
        }

        public void Delete(string title)
        {
            var shortages = GetAllShortages().ToList();
            var shortageToDelete = shortages.FirstOrDefault(s => s.Title == title);
            if (shortageToDelete == null)
            {
                Console.WriteLine("Shortage with this title doesn't exist!");
            }
            else
            {
                Console.WriteLine("Shortage was removed");
                shortages.Remove(shortageToDelete);
                SaveAllShortages(shortages);
            }
           
        }

        public IEnumerable<Shortage> GetAllShortages()
        {
            var content = File.ReadAllText(_filePath);
            var deserializedObject = JsonConvert.DeserializeObject<IEnumerable<Shortage>>(content);
            return deserializedObject.OrderByDescending(s => s.Priority);
        }

        public IEnumerable<Shortage> GetShortages(string title = null, string room = null, string category = null, DateTime? createdOnFrom = null, DateTime? createdOnTo = null)
        {
            var shortages = GetAllShortages();
            if (!string.IsNullOrWhiteSpace(title))
            {
                shortages = shortages.Where(s => s.Title.ToLower().Contains(title.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(room))
            {
                shortages = shortages.Where(s => s.Room.ToLower() == room.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                shortages = shortages.Where(s => s.Category.ToLower() == category.ToLower());
            }
            if (createdOnFrom.HasValue)
            {
                shortages = shortages.Where(s => s.CreatedOn >= createdOnFrom.Value);
            }
            if (createdOnTo.HasValue)
            {
                shortages = shortages.Where(s => s.CreatedOn <= createdOnTo.Value);
            }
            return shortages.OrderByDescending(s => s.Priority);
        }

        private void SaveAllShortages(IEnumerable<Shortage> shortages)
        {
            var json = JsonConvert.SerializeObject(shortages);
            File.WriteAllText(_filePath, json);
        }
    }
}
