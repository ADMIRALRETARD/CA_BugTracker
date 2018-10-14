using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class User
    {
        static int _id;
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User()
        {
            Id = ++_id;
        }
        
    }
}
