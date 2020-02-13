using System.Web.Optimization;

namespace AutomaticFootControl
{
    public class BundleConfig : System.Web.HttpApplication
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/css")
             .Include(
             "~/Content/assets/fonts/batch-icons/css/batch-icons.css",
             "~/Content/assets/css/bootstrap/bootstrap.min.css",
             "~/Content/assets/css/bootstrap/mdb.min.css",
             "~/Content/assets/plugins/custom-scrollbar/jquery.mCustomScrollbar.min.css",
             "~/Content/assets/css/hamburgers/hamburgers.css",
             "~/Content/assets/fonts/font-awesome/css/font-awesome.css",
             "~/Content/assets/plugins/jvmaps/jqvmap.min.css",
             "~/Content/assets/css/quillpro/quillpro.css",
             "~/Content/assets/FootControlCss/FootControl.css",
             "~/Content/assets/Gridmvc.css",
             "~/Content/assets/please-wait.css",
             "~/Content/assets/css/spinners/spinkit.css",
             "~/Content/assets/css/spinners/8-circle.css",
             "~/Content/assets/css/spinners/7-three-bounce.css",
             "~/Content/assets/gridmvc.datepicker.css",
             "~/Content/assets/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css",
             "~/Content/assets/datatable/jquery.dataTables.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/Scripts/jquery/jquery-3.1.1.min.js",
                "~/Content/assets/js/bootstrap/popper.min.js",
                "~/Content/assets/js/bootstrap/bootstrap.min.js",
                "~/Content/assets/js/bootstrap/mdb.min.js",
                "~/Content/assets/plugins/velocity/velocity.min.js",
                "~/Content/assets/plugins/velocity/velocity.ui.min.js",
                "~/Content/assets/plugins/custom-scrollbar/jquery.mCustomScrollbar.concat.min.js",
                "~/Content/assets/plugins/jquery_visible/jquery.visible.min.js",
                "~/Content/assets/plugins/jquery_visible/jquery.visible.min.js",
                "~/Content/assets/js/misc/ie10-viewport-bug-workaround.js",
                "~/Content/assets/plugins/chartjs/chart.bundle.min.js",
                "~/Content/assets/plugins/jvmaps/jquery.vmap.min.js",
                "~/Content/assets/plugins/jvmaps/maps/jquery.vmap.usa.js",
                "~/Content/assets/js/misc/holder.min.js",
                "~/Scripts/gridmvc.js",
                "~/Scripts/gridmvc.lang.ru.js",
                "~/Scripts/gridmvc.lang.fr.js",
                "~/Scripts/gridmvc.customwidgets.js",
                "~/Scripts/jsEsperando/footControl-esperando.js",
                "~/Scripts/jsEsperando/footControlHandler.js",
                "~/Scripts/jquer-validator/jquery.validate.min.js",
                "~/Scripts/please-wait/please-wait.min.js",
                "~/Scripts/jquery.redirect/jquery.redirect.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js",
                "~/Scripts/FootControl.js",
                "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.es.js",
                "~/Scripts/Inputmask/inputmask.js",
                "~/Scripts/Inputmask/inputmask.extensions.js",
                "~/Scripts/Inputmask/jquery.inputmask.js",
                "~/Content/assets/js/scripts.js",
                "~/Content/assets/datatable/jquery.dataTables.min.js",
                "~/Content/assets/Dateformat/dateformat.js",
                "~/Scripts/jsCanvas/imagen-canvas.js"));
        }
    }
}