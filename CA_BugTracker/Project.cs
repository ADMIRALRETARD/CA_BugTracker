using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Project
    {

        static int _id;

        public  int Id { get; private set; }


        public string ProjectName { get; set; }

        public Project()
        {
            Id=++_id;
            
        }

      

    }
}
