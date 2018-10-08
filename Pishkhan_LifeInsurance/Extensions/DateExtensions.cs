using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Extensions
{
    public static class DateExtensions
    {
        public static string DateToShamsi(this DateTime date) {

            try
            {
                PersianCalendar pc = new PersianCalendar();


                var day = pc.GetDayOfWeek(date);
                var month = pc.GetMonth(date);
                var year = pc.GetYear(date);

                return (year + "/" + month + "/" + day).ToString();
            }
            catch (Exception)
            {

                return "0000/00/00";
            }


        }

        public static DateTime DateToMiladi(this string date) {

            try
            {
                PersianCalendar pc = new PersianCalendar();
                string[] arr = date.Split("/");
                int year = int.Parse(arr[0]);
                int month = int.Parse(arr[1]);
                int day = int.Parse(arr[2]);

                DateTime dt = pc.ToDateTime( year, month, day, 0, 0, 0, 0);
                return dt;
            }
            catch 
            {

                return new DateTime();
            }
        }
    }
}
