using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Service.Setting
{
    /// <summary>
    /// The supported operations in where-extension
    /// </summary>
    public enum WhereOperation
    {
        [StringValue("eq")]
        Equal,
        [StringValue("ne")]
        NotEqual,
        [StringValue("cn")]
        Contains,
        [StringValue("bw")]
        Contains2
    }
}