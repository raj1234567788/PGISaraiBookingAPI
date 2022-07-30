using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace dll
{
    public class HacExceptionHnadler
    {
        public static void HandleException(Exception Ex)
        {

            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string sErrorTime = sYear + sMonth + sDay;
            LogException(Ex, sLogFormat, sErrorTime);

        }

        private static void LogException(Exception Ex, string sLogFormat, string sErrorTime)
        {
            String SystemPath = HttpContext.Current.Server.MapPath("~/ErrorLog/LogFileDated_");
            StreamWriter sw = new StreamWriter(SystemPath + sErrorTime, true);
            sw.WriteLine("*************************************************************");
            sw.WriteLine(sLogFormat);
            sw.WriteLine("**************Exception Message******************************");
            sw.WriteLine(Ex.Message.ToString());
            sw.WriteLine(Ex.StackTrace.ToString());
            sw.WriteLine(Ex.Source.ToString());
            sw.WriteLine("------------------------------------------------------------");

            sw.Flush();
            sw.Close();

        }
    }
}