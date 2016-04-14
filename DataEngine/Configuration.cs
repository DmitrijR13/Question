using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Sobits.DataEngine
{
    internal static class Configuration 
    {
        /// <summary>
        /// Use schema update
        /// </summary>
        public static Boolean UseSchemaUpdate
        {
            get
            {
                String value = ConfigurationManager.AppSettings["useSchemaUpdate"];

                if (!String.IsNullOrEmpty(value))
                {
                    return Boolean.Parse(value);
                }

                return false;
            }
        }

        /// <summary>
        /// Use data update
        /// </summary>
        public static Boolean UseDataCreate
        {
            get
            {
                String value = ConfigurationManager.AppSettings["useDataCreate"];

                if (!String.IsNullOrEmpty(value))
                {
                    return Boolean.Parse(value);
                }

                return false;
            }
        }
    }
}
