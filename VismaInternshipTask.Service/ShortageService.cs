using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaInternshipTask.Service
{
    using System;
    using System.Collections.Generic;

    namespace Visma.ResourceShortage.ConsoleApp
    {
        public class ShortageService : IShortageService
        {
            private readonly IShortageRepository _shortageRepository;

            public ShortageService(IShortageRepository shortageRepository)
            {
                _shortageRepository = shortageRepository;
            }

            public Shortage CreateShortage(string title, string name, string room, string category, int priority, string createdBy)
            {
                var shortage = new Shortage
                {
                    Title = title,
                    Name = name,
                    Room = room,
                    Category = category,
                    Priority = priority,
                    CreatedBy = createdBy
                };
                return _shortageRepository.Create(shortage);
            }

            public void DeleteShortage(string title, string room, string createdBy)
            {
                _shortageRepository.Delete(title, room, createdBy);
            }

            public IEnumerable<Shortage> GetAllShortages()
            {
                return _shortageRepository.GetAllShortages();
            }

            public IEnumerable<Shortage> GetShortages(string title = null, string room = null, string category = null, DateTime? createdOnFrom = null, DateTime? createdOnTo = null, string createdBy = null)
            {
                if (string.IsNullOrWhiteSpace(createdBy))
                {
                    return _shortageRepository.GetShortages(title, room, category, createdOnFrom, createdOnTo);
                }

                var allShortages = _shortageRepository.GetShortages(title, room, category, createdOnFrom, createdOnTo);
                return allShortages?.Where(s => s.CreatedBy == createdBy);
            }
        }
    }
}
