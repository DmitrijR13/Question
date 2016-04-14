using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Questionnaire.Service.Setting.Grid
{
    public class Filter
    {
        public string groupOp { get; set; }
       
        public Rule[] rules { get; set; }

        public static Filter Create(string jsonData)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Deserialize<Filter>(jsonData);
            }
            catch
            {
                return null;
            }
        }
    }
}