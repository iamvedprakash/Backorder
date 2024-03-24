using System.ComponentModel.DataAnnotations;

namespace Backorder.Models
{
    public class userrole
    {
        [Key]
        public int Is { get; set; }

        public string username { get; set; }

        public string role { get; set; }
    }
}
