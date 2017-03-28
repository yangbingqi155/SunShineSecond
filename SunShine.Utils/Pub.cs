using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace SunShine.Utils {
    public static class Pub
    {
        static AccessToken am;
        static JsAccessToKey jsam;
        static long t = DateTime.Now.Ticks / 10000;
        volatile static int un = 0;
        private volatile static object lk = new object();
        //id 生成器,CAS版本
        public static string ID()
        {
            //lock (lk)
            {
                long _t = DateTime.Now.Ticks / 10000;
                if (t == _t)
                {
                    //Interlocked.CompareExchange(ref t, _t, _t);
                    Interlocked.Increment(ref un);
                }
                else
                {
                    Interlocked.Exchange(ref t, _t);
                    Interlocked.Exchange(ref un, 0);
                }

                return t + "" + Thread.CurrentThread.ManagedThreadId + "" + un;
            }
        }
    }
}