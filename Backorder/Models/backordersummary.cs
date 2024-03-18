using System.ComponentModel.DataAnnotations;

namespace Backorder.Models
{
    public class backordersummary
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }
        public string Family { get; set; }
        public int QOH { get; set; }
        public int Price { get; set; }
        public int BO_Quantity { get; set; }
        public int BO_Amount { get; set; }
        
        public backordersummary(string item, string family, int QOH, int price, int BO_Quantity, int BO_Amount)
        {
            this.Item = item;
            this.Family = family;
            this.QOH = QOH;
            this.Price = price;
            this.BO_Quantity = BO_Quantity;
            this.BO_Amount = BO_Amount;
        }
        
    }
}
