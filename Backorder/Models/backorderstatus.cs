using System.ComponentModel.DataAnnotations;

namespace Backorder.Models
{
    public class backorderstatus
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }
        public string? Issue { get; set; }
        public string? Comment { get; set; }
        public DateTime? RecoveryDate { get; set; }
        public bool? POC { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        /*
        public backorderstatus(string item, string issue, string comment, DateOnly recoverydate, bool poc, string modifiedby, DateOnly modifieddate)
        {
            this.Item = item;
            this.Issue = issue;
            this.Comment = comment;
            this.RecoveryDate = recoverydate;
            this.POC = poc;
            this.ModifiedBy = modifiedby;
            this.ModifiedDate = modifieddate;

        }
        */
    }
}
