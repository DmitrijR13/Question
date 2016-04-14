using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Questionnaire.WebSite.Core;

namespace Questionnaire.WebSite.Core.Grid
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