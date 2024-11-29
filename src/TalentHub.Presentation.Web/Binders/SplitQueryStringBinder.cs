using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TalentHub.Presentation.Web.Binders;

public class SplitQueryStringBinder : IModelBinder
{
    private const char Separator = ',';
    private static readonly Dictionary<Type, Func<string, object>> Parsers = new()
    {
        [typeof(Guid)] = value => Guid.Parse(value),
        [typeof(string)] = value => value,
        [typeof(int)] = value => int.Parse(value)
    };

    public static void TryAddParser<TK>(Func<string, object> parser) =>
        Parsers.TryAdd(typeof(TK), parser);

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        Type collectionType = bindingContext.ModelType.GetGenericArguments().First();
        Type resultType = typeof(List<>).MakeGenericType(collectionType);
        object result = Activator.CreateInstance(resultType);
        MethodInfo addMethod = resultType.GetMethod(nameof(List<object>.Add))!;

        string composedParam = valueProviderResult.FirstValue;
        if (string.IsNullOrWhiteSpace(composedParam))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }

        try
        {
            if (!Parsers.TryGetValue(collectionType, out Func<string, object> parser))
            { throw new InvalidOperationException($"No parser found for {collectionType.Name}"); }

            IEnumerable<object> items =
                composedParam
                    .Split(Separator, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => Convert.ChangeType(parser(p), collectionType));

            foreach (object item in items)
            { addMethod.Invoke(result, [item]); }

            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch (FormatException)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid type format.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
