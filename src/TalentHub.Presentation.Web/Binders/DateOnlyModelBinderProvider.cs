using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TalentHub.Presentation.Web.Binders;

public sealed class DateOnlyModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
        context.Metadata.ModelType == typeof(DateOnly)
            ? new BinderTypeModelBinder(typeof(DateOnlyModelBinder))
            : null;
}