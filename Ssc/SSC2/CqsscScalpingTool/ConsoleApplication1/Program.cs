using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
         



        }
    }

    public class Debit
    {
        public DateTime Start { get; set; }
        public string BankName { get; set; }
        public float MonthlyRate { get; set; }
        public float YearlyRate { get; set; }

        public float HavePaid { get; set; }

        public float Unpaid { get; set; }
    }
}
