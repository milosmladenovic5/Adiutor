using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Buisiness;

namespace MvcIng.Logic
{
    public class Authorize : AuthorizeAttribute
    {
        private Definitions.PermissionKeys[] _allowedPermissions;

        public Definitions.PermissionKeys AllowedPermission;
        public Definitions.PermissionKeys[] AllowedPermissions
        {
            get { return _allowedPermissions ?? (_allowedPermissions = new[] { AllowedPermission }); }
            set { _allowedPermissions = value; }
        }

        public bool AllMustBeGranted { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return IsAuthorized();
        }

        private bool IsAuthorized()
        {
            return AllMustBeGranted
                       ? AllowedPermissions
                             .All(PermissionManager.IsPermissionGranted)
                       : AllowedPermissions
                             .Any(PermissionManager.IsPermissionGranted);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // if regular http request (NOT AJAX)
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                base.HandleUnauthorizedRequest(filterContext);
                return;
            }

            filterContext.Result = new JsonResult
            {
                Data = new { AjaxError = "LoggedOut" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
