using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class hierarchical
    {
        public int ID { get; set; }

        //Cat Name
         [DisplayName("Office Name")]
        public string Name { get; set; }

        //Cat Description  
        public string Description { get; set; }

        //represnts Parent ID and it's nullable  
        [DisplayName("Parent Office")]
       // public int? Pid { get; set; }
        [ForeignKey("Pid")]
        public int? Pid { get; set; }


        //public virtual hierarchical Parent { get; set; }

        public Parent Parent { get; set; }

        internal object gethierarchical()
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<hierarchical> Childs { get; set; }
    }
}
