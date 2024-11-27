using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TalentHub.Presentation.Web.Binders;

public class SplitQueryStringBinder(char separator = ',') : IModelBinder
{
    private static readonly Dictionary<Type, Func<string, object>> _parsers = new()
    {  
        [typeof(Guid)] = value => Guid.Parse(value),
        [typeof(string)] = value => value,
        [typeof(int)] = value => int.Parse(value)
    };

    public static void TryAddParser<K>(Func<string, object> parser) 
    {
        _parsers.TryAdd(typeof(K), parser);
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(Array.Empty<object>());
            return Task.CompletedTask;
        }

        try
        {
            var collectionType = bindingContext.ModelType.GetGenericArguments().First();

            var splitValues = value.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(_parsers[collectionType])
                .ToArray();

            bindingContext.Result = ModelBindingResult.Success(splitValues);
        }
        catch (FormatException)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid type format.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
