using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoScalping.Util;

namespace AutoScalping
{
    public partial class fmsscMain : Form
    {
        private string CurrentUrlLocalPath { get; set; }

        public fmsscMain()
        {
            InitializeComponent();
        }

        private void fmsscMain_Load(object sender, EventArgs e)
        {
            LoginJiangCom();
        }

        private void LoginJiangCom()
        {
            //this.wb1.Navigate("http://www.jiang.com/statichtml/register.html");
            //this.wb1.Navigate("http://m.jiang.com/User/login");
            this.wb1.Navigate("http://www.jiang.com/szc/cqssc");
        }

        private void wb1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.CurrentUrlLocalPath = e.Url.LocalPath.ToLower();
            if (this.CurrentUrlLocalPath == "/statichtml/register.html".ToLower())
            {
                if (this.wb1.ReadyState == WebBrowserReadyState.Complete)
                {
                    this.wb1.DoAction("loginbtn_new", "click");
                    System.Threading.Thread.Sleep(5000);
                    MessageBox.Show("点击确认登录");

                    this.wb1.Document.All["lu"].InnerText = "迦美妈妈";
                    this.wb1.Document.GetElementById("lp").SetAttribute("value", "123123");

                    //this.wb1.SetInputValue("username", "迦美妈妈");
                    //this.wb1.SetInputValue("password", "123123");
                    //this.wb1.SetInputValue(new KeyValuePair<string, string>[]
                    //{
                    //    new KeyValuePair<string, string>("lu","迦美妈妈"),
                    //    new KeyValuePair<string, string>("lp","123123"),
                    //});
                    //var ele = this.wb1.Document.GetElementById("lu");
                    //var s = ele.OuterHtml;
                    //ele.InnerText = "asdfasdfsadfsad";

                    this.wb1.DoAction("floginbtn", "click");

                    System.Threading.Thread.Sleep(5000);
                    this.wb1.Navigate("http://www.jiang.com/szc/cqssc");
                }
            }
            //
            if (this.CurrentUrlLocalPath == "/szc/cqssc".ToLower())
            {
                if (this.wb1.ReadyState == WebBrowserReadyState.Complete)
                {
                    //index  0：一星单选   8：大小单双
                    this.wb1.Document.GetElementById("GameTypeList").GetElementsByTagName("a")[0].InvokeMember("click");
                  System.Threading.Thread.Sleep(5000);

                    var balls =this.wb1.Document.GetElementById("CQSSC_1XDX").GetElementsByTagName("ul")[0].GetElementsByTagName("b");
                    balls[0].InvokeMember("click");
                    balls[2].InvokeMember("click");
                    balls[4].InvokeMember("click");

                    this.wb1.Document.GetElementById("addToListBtn").InvokeMember("click");

                    this.wb1.Document.GetElementById("multipleInfo").GetElementsByTagName("input")[1].SetAttribute("value","99");



                }
            }



            //
        }
    }
}
