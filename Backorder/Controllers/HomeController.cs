using Backorder.Data;
using Backorder.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Backorder.Controllers
{
    public class HomeController : Controller
    {
        private readonly backorderappcontext _context;

        public HomeController(backorderappcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var backordersummary = _context.backordersummary.ToList();
            return View(backordersummary);
        }

        public IActionResult getItemStatus(string item)
        {
            var itemstatus = _context.backorderstatus.FirstOrDefault(m => m.Item == item);
            return Json(itemstatus);
        }

        public void updateItemStatus(string item, string? issue, string? comment, DateTime? recoverydate, bool? POC)
        {
            var itemrow = _context.backorderstatus.FirstOrDefault(m => m.Item == item);

            itemrow.Issue         = issue;
            itemrow.Comment       = comment;
            itemrow.RecoveryDate  = recoverydate;
            itemrow.POC           = POC;
            itemrow.ModifiedBy    = "System";
            itemrow.ModifiedDate  = DateTime.Now;

            _context.Update(itemrow);
            _context.SaveChanges();
        }

        public void exportData()
        {
            using (var context = new backorderappcontext())
            {
                var query = from su in context.backordersummary
                            join st in context.backorderstatus
                            on su.Item equals st.Item into gj
                            from status in gj.DefaultIfEmpty()
                            select new
                            {
                                su.Id,
                                su.Item,
                                su.Family,
                                su.QOH,
                                su.Price,
                                su.BO_Quantity,
                                su.BO_Amount,
                                StatusIssue         = status != null ? status.Issue :        null,
                                StatusComment       = status != null ? status.Comment :      null,
                                StatusRecoveryDate  = status != null ? status.RecoveryDate : null,
                                StatusPOC           = status != null ? status.POC :          null,
                                StatusModifiedBy    = status != null ? status.ModifiedBy :   null,
                                StatusModifiedDate  = status != null ? status.ModifiedDate : null
                            };

                var result = query.ToList();


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Backorder Items");

                    worksheet.Cell(1, 1).Value  = "Item";
                    worksheet.Cell(1, 2).Value  = "Family";
                    worksheet.Cell(1, 3).Value  = "QOH";
                    worksheet.Cell(1, 4).Value  = "Price";
                    worksheet.Cell(1, 5).Value  = "BO_Quantity";
                    worksheet.Cell(1, 6).Value  = "BO_Amount";
                    worksheet.Cell(1, 7).Value  = "Issue";
                    worksheet.Cell(1, 8).Value  = "Comment";
                    worksheet.Cell(1, 9).Value  = "Recovery Date";
                    worksheet.Cell(1, 10).Value = "POC";
                    worksheet.Cell(1, 11).Value = "Modified By";
                    worksheet.Cell(1, 12).Value = "Modified Date";

                    int row = 2;

                    foreach(var items in result)
                    {
                        worksheet.Cell(row, 1).Value  = items.Item;
                        worksheet.Cell(row, 2).Value  = items.Family;
                        worksheet.Cell(row, 3).Value  = items.QOH;
                        worksheet.Cell(row, 4).Value  = items.Price;
                        worksheet.Cell(row, 5).Value  = items.BO_Quantity;
                        worksheet.Cell(row, 6).Value  = items.BO_Amount;
                        worksheet.Cell(row, 7).Value  = items.StatusIssue;
                        worksheet.Cell(row, 8).Value  = items.StatusComment;
                        worksheet.Cell(row, 9).Value  = items.StatusRecoveryDate;
                        if(items.StatusPOC == true)
                        {
                            worksheet.Cell(row, 10).Value = "True";
                        }
                        else
                        {
                            worksheet.Cell(row, 10).Value = "False";
                        }
                        worksheet.Cell(row, 11).Value = items.StatusModifiedBy;
                        worksheet.Cell(row, 12).Value = items.StatusModifiedDate;

                        row++;
                    }

                    
                    workbook.SaveAs("../Backorder Items.xlsx");

                }

            }

        }

        public void setSummaryData()
        {
            List<backordersummary> temp = new List<backordersummary>();

            temp.Add(new backordersummary("00-1653-01", "cosmetics", 119, 500, 909, 454500));
            temp.Add(new backordersummary("00-1653-02", "cosmetics", 25, 800, 1748, 1398400));
            temp.Add(new backordersummary("00-1653-03", "cosmetics", 30, 300, 432, 129600));
            temp.Add(new backordersummary("00-1653-04", "haircare", 55, 900, 1022, 919800));
            temp.Add(new backordersummary("00-1653-05", "haircare", 128, 1400, 1373, 1922200));
            temp.Add(new backordersummary("00-1653-06", "haircare", 25, 800, 1524, 1219200));
            temp.Add(new backordersummary("00-1653-07", "skincare", 95, 100, 137, 13700));
            temp.Add(new backordersummary("00-1653-08", "skincare", 48, 2500, 895, 2237500));
            temp.Add(new backordersummary("00-1653-09", "skincare", 56, 2000, 1087, 2174000));
            temp.Add(new backordersummary("00-1654-01", "skincare", 66, 1700, 501, 851700));
            temp.Add(new backordersummary("00-1654-02", "skincare", 66, 600, 1386, 831600));
            

            for(int i=0;i<temp.Count;i++)
            {
                _context.Add(temp[i]);
                _context.SaveChanges();
            }

        }

    }
}