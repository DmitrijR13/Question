using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Core
{
    /// <summary>
    /// Lazy loader
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lazy<T>
        where T: class
    {
        public Lazy()
        { 
        }

        public Lazy(Func<T> constructor)
        {
            _constructor = constructor;
        }

        public T Instance
        {
            get
            {
                if (_constructor != null)
                {
                    _instance = _constructor();
                }

                return _instance;
            }
        }

        #region private fields

        private T _instance;
        private readonly Func<T> _constructor;

        #endregion private fields
    }
}