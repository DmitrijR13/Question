using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Service.Setting.Grid
{
    public class GridResult<T> where T : class
    {
        public Int32 total { get; set; }
        public Int32 page { get; set; }
        public Int32 records { get; set; }
        public List<T> rows { get; set; }
    }
}