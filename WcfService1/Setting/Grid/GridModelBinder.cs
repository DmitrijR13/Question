using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Questionnaire.Service.Setting;

namespace Questionnaire.Service.Setting.Grid
{
    public class GridModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var request = controllerContext.HttpContext.Request;
                GridOptions options = new GridOptions
                {
                    Operation = (GridOperations)StringEnum.Parse(typeof(GridOperations), request["oper"] ?? "none"),
                    IsSearch = Boolean.Parse(request["_search"] ?? "false"),
                    PageIndex = Int32.Parse(request["page"] ?? "-1"),
                    PageSize = Int32.Parse(request["rows"] ?? "-1"),
                    SortColumn = request["sidx"] ?? "",
                    SortOrder = request["sord"] ?? "",
                    Where = Filter.Create(request["filters"] ?? ""),
                    ND = Int64.Parse(request["nd"] ?? "-1")
                };
                
                return options;
            }
            catch
            {
                return null;
            }
        }
    }
}