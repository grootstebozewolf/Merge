using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4NetLogger
{
    /// <summary>
    /// Generic class implements singleton pattern.
    /// </summary>
    /// <typeparam name="T">
    /// Reference type. Important: Must have protected constructor (not public).
    /// </typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                //lock the instance type
                lock (typeof(T))
                {
                    //create instance if not exist
                    if (instance == null)
                    {
                        instance = new T();
                    }
                    //return instance
                    return instance;
                }
            }
        }
    }
}
