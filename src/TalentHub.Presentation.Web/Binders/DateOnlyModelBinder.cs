using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TalentHub.Presentation.Web.Binders;

public sealed class DateOnlyModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        string? value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        
        if (string.IsNullOrEmpty(value))
        { return Task.CompletedTask; }
        
        if (DateOnly.TryParse(value, out DateOnly dateOnly))
        { bindingContext.Result = ModelBindingResult.Success(dateOnly); }
        else
        { bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid date format."); }
        
        return Task.CompletedTask;
    }
}

public sealed class DateOnlyModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
        context.Metadata.ModelType == typeof(DateOnly)
        ? new BinderTypeModelBinder(typeof(DateOnlyModelBinder))
        : null;
}
