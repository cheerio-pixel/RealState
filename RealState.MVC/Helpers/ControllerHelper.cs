using System.Collections.Specialized;

using Microsoft.AspNetCore.Mvc;

namespace RealState.MVC.Helpers
{
    public static class ControllerHelper
    {
        public static IActionResult RedirectBack(this Controller self, NameValueCollection? query = null)
        {
            string? referer = self.Request.Headers.Referer.ToString().Split("?").FirstOrDefault();
            return self.Redirect(string.Join("?", referer, query?.ToString()));
        }
    }

}