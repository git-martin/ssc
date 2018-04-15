using System.Data;
using System.IO;

namespace AutoScalping.Util
{
    public class CsvFileReader
    {
        public static DataTable Parse(string data, bool headers)
        {
            return Parse(new StringReader(data), headers);
        }

        public static DataTable Parse(string data)
        {
            return Parse(new StringReader(data));
        }

        public static DataTable Parse(TextReader stream)
        {
            return Parse(stream, false);
        }

        public static DataTable Parse(TextReader stream, bool headers)
        {
            DataTable table = new DataTable();

            CsvFileStream csv = new CsvFileStream(stream);
            string[] row = csv.GetNextRow();
            if (row == null) return null;

            if (headers)
            {
                foreach (string header in row)
                {
                    if (header != null && header.Length > 0 && !table.Columns.Contains(header))
                        table.Columns.Add(header, typeof(string));
                    else
                        table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                }
                row = csv.GetNextRow();
            }

            while (row != null)
            {
                while (row.Length > table.Columns.Count)
                    table.Columns.Add(GetNextColumnHeader(table), typeof(string));
                table.Rows.Add(row);
                row = csv.GetNextRow();
            }
            return table;
        }

        static string GetNextColumnHeader(DataTable table)
        {
            int c = 1;
            while (true)
            {
                string h = "Column" + c++;
                if (!table.Columns.Contains(h))
                    return h;
            }
        }
    }
}
