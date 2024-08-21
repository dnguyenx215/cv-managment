using CVManagementAPI.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CVManagementAPI.Helpers
{
    public class ExcelExport
    {
        public static async Task<Stream> ExportToExcelHelper(List<CV> listCV)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Stream stream = new MemoryStream();
            var propertyList = listCV;
            using (var package = new ExcelPackage(stream))
            {
                var currentRow = 1;
                var currentColumn = 1;
                // thêm tiêu đề
                var type = typeof(CV);
                var properties = type.GetProperties();
                Console.WriteLine(properties.Length);
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(propertyList, true);
                for (int i = 0; i < properties.Length; i++)
                {
                    var propertyName = properties[i].Name;

                    if (propertyName.Contains("Id"))
                    {
                        workSheet.Columns[i + 1].Width = 35;
                    }
                    else
                    {
                        workSheet.Columns[i + 1].Width = 25;
                    }

                    workSheet.Cells[1, i + 1].Style.Font.Bold = true;
                    // Assign borders
                    workSheet.Cells[1, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    workSheet.Cells[1, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    workSheet.Cells[1, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    workSheet.Cells[1, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                    workSheet.Cells[1, i + 1].Style.Font.UnderLine = true;
                    workSheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    workSheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                }

                for (int i = 1; i <= propertyList.Count; i++)
                    for (int j = 0; j < properties.Length; j++)
                    {
                        // Assign borders
                        workSheet.Cells[i + 1, j + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 1, j + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 1, j + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 1, j + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}