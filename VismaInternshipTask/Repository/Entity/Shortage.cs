using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaInternshipTask.Repository.Entity
{
    public class Shortage
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }

    }

}
