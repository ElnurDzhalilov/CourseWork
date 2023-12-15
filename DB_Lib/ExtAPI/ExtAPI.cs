using DB_Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lib.ExtAPI
{
    public static class ExtAPI
    {
        //public void WriteExceptionLog(string message)
        //{
        //    //int savedCnt = 0;

        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        ExceptionLog ex = new ExceptionLog();
        //        ex.Message = message;

        //        db.ExceptionLog.Add(ex);
        //        db.SaveChanges();
        //        //savedCnt = db.SaveChanges();
        //    }

        //    //return savedCnt > 0;
        //}

        public static void WriteCalcRes(double LBorder, double RBorder, string Foo, string Result)
        {
            //int savedCnt = 0;

            using (ApplicationContext db = new ApplicationContext())
            {
                CalcRes res = new CalcRes()
                {
                    LBorder = LBorder,
                    RBorder = RBorder,
                    Foo = Foo,
                    Result = Result,
                };

                db.CalcRes.Add(res);
                db.SaveChanges();
                //savedCnt = db.SaveChanges();
            }

            //return savedCnt > 0;
        }

        public static List<CalcRes> GetCalcRes()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.CalcRes.ToList();
            }


            //ApplicationContext db = new ApplicationContext();
            //var list = new List<CalcRes>();

            //try
            //{
            //    list = db.CalcRes.ToList();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    db.Dispose();
            //}

            //return list;
        }
    }
}
