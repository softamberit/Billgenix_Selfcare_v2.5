﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare
{
    class WriteLogFile
    {
        public static bool WriteLog(string strMessage)
        {
            //string strFileName = HttpContext.Current.Server.MapPath("~/logTemp.txt");
            //strMessage = DateTime.Now.ToString() + " " + strMessage;


            //try
            //{
            //    FileStream objFilestream = new FileStream(string.Format("{0}", strFileName), FileMode.Append, FileAccess.Write);
            //    StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            //    objStreamWriter.WriteLine(strMessage);
            //    objStreamWriter.Close();
            //    objFilestream.Close();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            return false;
        }
    }  
}