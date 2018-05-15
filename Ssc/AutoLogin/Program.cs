using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLogin
{
    class Program
    {
        static void Main(string[] args)
        {

            var myclient = new MyWebClient(60);
            var err = "";
            //http://www.chachongdao.com/user/UserCenter.aspx
           var s= myclient.GetSrc("http://www.chachongdao.com", null, out err);

            Console.ReadLine();
        }
    }
}
