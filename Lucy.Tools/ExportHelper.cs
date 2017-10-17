
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
  public  class ExportHelper
    {
        public static ExportHelper Instance { get { return new ExportHelper(); } }
        const int COUNT = 1024;
        public ResultEx GenerationExcelByDataTable(string url, DataTable table)
        {
            try
            {
                var stream = DataTableToSteam(table);
                using (var file = System.IO.File.Open(url, System.IO.FileMode.OpenOrCreate))
                {
                    var buffer = new byte[COUNT];
                    var count = 0;

                    while ((count = stream.Read(buffer, 0, COUNT)) > 0)
                    {
                        file.Write(buffer, 0, count);
                    }
                }

                return ResultEx.Init();
            }
            catch (Exception ex)
            {
                return ResultEx.Init(ex);
            }
        }
        public Stream DataTableToSteam(DataTable table, string[] fields)
        {
            var temp = table.Copy();

            for (var i = 0; i < fields.Length; i++)
            {
                if (temp.Columns.Count > i)
                {
                    temp.Columns[i].ColumnName = fields[i];
                }
            }

            return DataTableToSteam(temp);
        }
        public Stream DataTableToSteam(DataTable table)
        {
            MemoryStream ms = new MemoryStream();

            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);

                // handling header.
                foreach (DataColumn column in table.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value

                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }

            return ms;
        }
    }
}
