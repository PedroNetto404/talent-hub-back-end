using Microsoft.AspNetCore.Mvc.ModelBinding;

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