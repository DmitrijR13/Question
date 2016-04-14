using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Sobits.Story.Logic
{
    /// <summary>
    /// Project configuration
    /// </summary>
    public static class Configuration
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
        /// Use create data
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

        /// <summary>
        /// File backup
        /// </summary>
        public static String FileBackup
        {
            get
            {
                String value = ConfigurationManager.AppSettings["fileBackup"];

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                return "C:/backup/new_backup.bak";
            }
        }

        /// <summary>
        /// Path DB local
        /// </summary>
        public static String DbLocal
        {
            get
            {
                String value = ConfigurationManager.AppSettings["pathDBLocal"];

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Connection string DB local
        /// </summary>
        public static String DbLocalConnectionString
        {
            get
            {
                String value = ConfigurationManager.ConnectionStrings["LocalDBConnectionString"].ConnectionString;

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                return String.Empty;
            }
        }
    }
}
