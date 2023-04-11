using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaInternshipTask.Repository;
using VismaInternshipTask.Repository.Entity;

namespace VismaInternshipTask.Service
{
    public class ShortageService : IShortageService
    {
        private readonly IShortageRepository _shortageRepository;

        public ShortageService(IShortageRepository shortageRepository)
        {
            _shortageRepository = shortageRepository;
        }

        public Shortage CreateShortage(string title, string name, string room, string category, int priority)
        {
            var shortage = new Shortage
            {
                Title = title,
                Name = name,
                Room = room,
                Category = category,
                Priority = priority
            };
            return _shortageRepository.Create(shortage);
        }

        public void DeleteShortage(string title)
        {
            _shortageRepository.Delete(title);
        }

        public IEnumerable<Shortage> GetAllShortages()
        {
            return _shortageRepository.GetAllShortages();
        }

        public IEnumerable<Shortage> GetShortages(string title = null, string room = null, string category = null, DateTime? createdOnFrom = null, DateTime? createdOnTo = null)
        {
            var allShortages = _shortageRepository.GetShortages(title, room, category, createdOnFrom, createdOnTo);
            return allShortages;
        }
    }
}
