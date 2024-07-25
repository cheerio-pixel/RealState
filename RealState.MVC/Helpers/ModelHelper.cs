using Microsoft.AspNetCore.Mvc.ModelBinding;

using RealState.Application.Extras.ResultObject;

namespace RealState.MVC.Helpers
{
    internal static class ModelHelper
    {
        public static ModelStateDictionary AggregateErrors(this ModelStateDictionary self, IEnumerable<AppError>? errors)
        {
            if (errors is not null)
            {
                foreach (var a in errors)
                {
                    self.AddModelError(a.PropertyName, a.Message);
                }
            }
            return self;
        }

        public static IEnumerable<string> GetErrors(this ModelStateDictionary self)
        => self.SelectMany(a => a.Value?.Errors.Select(a => a.ErrorMessage) ?? Array.Empty<string>());
    }
}
