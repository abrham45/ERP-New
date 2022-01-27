using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; }
        public string  Subject { get; set; }
        public string Body { get; set; }

        public UserEmailOptions(IEnumerable<string> to, string sub, string body) 
        {
            to = new List<string>();
            sub = Subject;
            body = Body;
        }
    }
}
