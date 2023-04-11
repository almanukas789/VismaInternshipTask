using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaInternshipTask.Repository.Entity;

namespace VismaInternshipTask.Service
{
    public interface IShortageService
    {
        Shortage CreateShortage(string title, string name, string room, string category, int priority);
        void DeleteShortage(string title);
        IEnumerable<Shortage> GetAllShortages();
        IEnumerable<Shortage> GetShortages(string title = null, string room = null, string category = null, DateTime? createdOnFrom = null, DateTime? createdOnTo = null);

    }
}
