using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoScalping.Models;

namespace AutoScalping.Controller
{
    public class QcwComController:BaseController
    {
        public static void Login(ref AccountResponse qcwAccount)
        {
            try
            {
                Login(ref qcwAccount, GloableConstants.AccountBelongs.Qcw);
                GloableParams.CurrentAccount = qcwAccount;
            }
            catch (Exception ex)
            {
                qcwAccount.HasError = true;
                qcwAccount.ErrorMessage = "账号登录异常：" + ex.Message;
                //MessageBox.Show(ex.Message);
            }
        }

        public static void BettingYiXing(ref BettingSolutinModel model, string userToken)
        {
            try
            {
                BettingYiXing(ref model, GloableConstants.AccountBelongs.Qcw, userToken);
            }
            catch (Exception ex)
            {
                model.IsBettingSucc = false;
                model.MyMessage = "投注异常：" + ex.Message;
            }
        }
    }
}
