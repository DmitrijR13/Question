using System.Web;
using System.Web.Optimization;

namespace Questionnaire.WebSite
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {  
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.10.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsproj").Include(
                        "~/Scripts/default.js",
                        "~/Scripts/jquery.fileupload.js",
                        "~/Scripts/jquery.iframe-transport.js",
                        "~/Scripts/jquery.ui.datepicker.js",
                        "~/Scripts/jquery-ui-timepicker-addon.js",
                        "~/Scripts/datepicker.ru.js",
                        "~/Scripts/jquery.ui.timepicker.js",
                        "~/Scripts/serviceWindow.js",
                        "~/Scripts/Grid/grid.locale-ru.js",
                        "~/Scripts/Grid/jquery.jqGrid.src.js",
                        "~/Scripts/jquery.editable-select.js",
                        "~/Scripts/select2.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/redmond/jquery-ui-1.10.2.custom*",
                        "~/Content/ui.jqgrid.css",
                        "~/Content/ajaxfileupload.css",
                        "~/Content/jquery.editable-select.css",
                        "~/Content/select2.css"));

            bundles.Add(new StyleBundle("~/Content/unblind/css").Include(
                        "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/blind/css").Include(
                        "~/Content/styleForBlind.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/datetimepicker/jquery.ui.timepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}