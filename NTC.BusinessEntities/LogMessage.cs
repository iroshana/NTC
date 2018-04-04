using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class LogMessage
    {
        public long ID { get; set; }

        [Required]
        [StringLength(500)]
        public string ApplicationName { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [StringLength(256)]
        public string User { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
