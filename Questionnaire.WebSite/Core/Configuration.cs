using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Questionnaire.WebSite.Core
{
    public static class Configuration
    {
        /// <summary>
        /// Crypt password
        /// </summary>
        public static String CryptPassword
        {
            get
            {
                return "ValarMorgulis";
            }
        }

        /// <summary>
        /// Cookie expiration period
        /// </summary>
        public static Int32 CookieExpiresDays
        {
            get
            {
                String value = ConfigurationManager.AppSettings["cookieExpiresDays"];

                if (!String.IsNullOrEmpty(value))
                {
                    return Int32.Parse(value);
                }

                return 14;
            }
        }

        /// <summary>
        /// Path to file folder
        /// </summary>
        public static String PathFileFolder
        {
            get
            {
                String value = ConfigurationManager.AppSettings["folderFile"];

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                return "D:/Files/Template/";
            }
        }

        public static String DBExternalConnectionString
        {
            get
            {
                String value = ConfigurationManager.ConnectionStrings["ExternalDBConnectionString"].ConnectionString;

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                return String.Empty;
            }
        }
    }
}