using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using AutoScalping.Models;
using AutoScalping.Util;

namespace AutoScalping.Controller
{
    public class JiangComController: BaseController
    {
        public static void Login(ref AccountResponse jiangAccount)
        {
            try
            {
                Login(ref jiangAccount, GloableConstants.AccountBelongs.Jiang);   
                //GloableParams.CurrentAccount = jiangAccount;
            }
            catch (Exception ex)
            {
                jiangAccount.HasError = true;
                jiangAccount.ErrorMessage = "账号登录异常：" + ex.Message;
                //MessageBox.Show(ex.Message);
            }
        }

        public static void BettingYiXing(ref BettingSolutinModel model,string userToken)
        {
            try
            {
                BettingYiXing(ref model, GloableConstants.AccountBelongs.Jiang,userToken);
            }
            catch (Exception ex)
            {
                model.IsBettingSucc = false;
                model.MyMessage = "投注异常：" + ex.Message;
            }
        }


    }
}
