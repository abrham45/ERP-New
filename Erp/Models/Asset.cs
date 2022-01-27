using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Asset
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public string Asset_Name{ get; set; }
        [Required]
        public string factory_number { get; set; }
        public string serial_number { get; set; }
        [Required]
        public Decimal Price{ get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public int Asset_typeId { get; set; }
       // public Payment Payment { get; set; }
        public Supplier Supplier { get; set; }
        public Asset_type Asset_type { get; set; }

        internal static object AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
