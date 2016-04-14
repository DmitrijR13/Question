using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Questionnaire.Service.Setting;

namespace Questionnaire.Service.Setting.Grid
{
    public enum GridOperations
    {
        [StringValue("add")]
        Create,
        [StringValue("edit")]
        Edit,
        [StringValue("del")]
        Delete,
        [StringValue("none")]
        None
    }
}