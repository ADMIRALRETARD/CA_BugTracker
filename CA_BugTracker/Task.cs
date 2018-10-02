using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Task
    {

        static int _id;
        public int Id { get; private set; }
        public string Theme { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }


        public User User { get; set; }
        public Project Project { get; set; }

        public Task()
        {
            Id = ++_id;

        }

    }
}
