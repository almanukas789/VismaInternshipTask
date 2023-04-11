using VismaInternshipTask.Repository.Entity;

namespace VismaInternshipTask.Repository
{
    public interface IShortageRepository
    {
        Shortage Create(Shortage shortage);
        void Delete(string title);
        IEnumerable<Shortage> GetAllShortages();
        IEnumerable<Shortage> GetShortages(string title = null, string room = null, string category = null, DateTime? createdOnFrom = null, DateTime? createdOnTo = null);
        
    }

}
