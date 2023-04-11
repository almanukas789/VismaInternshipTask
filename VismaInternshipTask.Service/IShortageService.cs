using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaInternshipTask;

namespace VismaInternshipTask.Service
{
    public interface IShortageService
    {
        void AddShortage(Shortage shortage);
        void DeleteShortage(int id, string createdBy);
        List<Shortage> GetAllShortages(string userRole, string createdBy = null);
        List<Shortage> GetShortagesByCategory(string category);
        List<Shortage> GetShortagesByCreatedOn(DateTime startDate, DateTime endDate);
        List<Shortage> GetShortagesByRoom(string room);
        List<Shortage> GetShortagesByTitle(string title);
        void UpdateShortage(int id, Shortage shortage);
    }

}
