using System.Text;
using System.Text.RegularExpressions;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Core.Results;

public partial class Result
{
    private readonly Error? _error;

    protected Result(Error error) => _error = error;
    protected Result() => _error = null;

    public Error Error =>
        IsFail
        ? _error!
        : throw new InvalidOperationException("Successfully result cannot hava error");

    public bool IsFail => _error is not null;
    public bool IsOk => !IsFail;

    public static Result Ok() => new();
    public static Result<T> Ok<T>(T value) where T : notnull => new(value);
    public static Result Fail(Error error) => new(error);
    public static Result<T> Fail<T>(Error error) where T : notnull => new(error);

    public static implicit operator Result(Error error) => new(error);

    public static Result FailEarly(params Func<Result>[] funcs)
    {
        foreach (Func<Result> func in funcs)
        {
            if (func() is { IsFail: true } result)
            {
                return result;
            }
        }

        return Ok();
    }

    public static Result FailIf(bool condition, string erroMessage) =>
        condition
        ? Fail(Error.BadRequest(erroMessage))
        : Ok();

    public static Result FailIfIsNullOrWhiteSpace(string value, string errorMessage) =>
        FailIf(string.IsNullOrWhiteSpace(value), errorMessage);

    public static Result FailIfEquals<T>(T value, T other, string errorMessage) where T : notnull =>
        FailIf(value.Equals(other), errorMessage);

    public static Result FailIfNotEmail(string email)
    {
        if (!EmailValidationRegex().IsMatch(email))
        {
            return Error.BadRequest("invalid email");
        }

        return Ok();
    }

    public static Result FailIfNotUrl(string url) =>
        url.IsValidUrl() ? Ok() : Fail(Error.BadRequest("invalid url"));

    public static Result FailIfNotCpfj(string value)
    {
        string cleanCnpj = RemoveNonNumeric(value);

        if (cleanCnpj.Length != 14 || !CnpjRegex().IsMatch(cleanCnpj))
        {
            return Fail(Error.BadRequest("CNPJ inválido."));
        }

        if (!IsValidCnpj(cleanCnpj))
        {
            return Fail(Error.BadRequest("CNPJ inválido."));
        }

        return Ok();
    }

    public static Result FailIfNotPhone(string value)
    {
        string cleanPhone = RemoveNonNumeric(value);

        if (cleanPhone.Length < 10 || cleanPhone.Length > 11)
        {
            return Fail(Error.BadRequest("Telefone inválido."));
        }

        return Ok();
    }

    public static Result FailIfNotCfp(string value)
    {
        string cleanCpf = RemoveNonNumeric(value);

        if (cleanCpf.Length != 11 || !CpfRegex().IsMatch(cleanCpf))
        {
            return Fail(Error.BadRequest("CPF inválido."));
        }
        
        if (!IsValidCpf(cleanCpf))
        {
            return Fail(Error.BadRequest("CPF inválido."));
        }

        return Ok();
    }

    private static string RemoveNonNumeric(string value)
    {
        var sb = new StringBuilder();
        foreach (char c in value)
        {
            if (char.IsDigit(c))
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    private static bool IsValidCpf(string cpf)
    {
        var invalidCpfs = new HashSet<string>
        {
            "00000000000", "11111111111", "22222222222", "33333333333",
            "44444444444", "55555555555", "66666666666", "77777777777",
            "88888888888", "99999999999"
        };

        if (invalidCpfs.Contains(cpf))
        {
            return false;
        }

        int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        if (!CalculateVerifierDigits(cpf, multiplier1, multiplier2, 9))
        {
            return false;
        }

        return true;
    }

    private static bool IsValidCnpj(string cnpj)
    {
        int[] multiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        if (!CalculateVerifierDigits(cnpj, multiplier1, multiplier2, 12))
        {
            return false;
        }

        return true;
    }

    private static bool CalculateVerifierDigits(string value, int[] multiplier1, int[] multiplier2, int length)
    {
        int sum = 0;
        for (int i = 0; i < length; i++)
        {
            sum += (value[i] - '0') * multiplier1[i];
        }

        int remainder = sum % 11;
        int digit1 = remainder < 2 ? 0 : 11 - remainder;

        sum = 0;
        for (int i = 0; i < length + 1; i++)
        {
            sum += (value[i] - '0') * multiplier2[i];
        }

        remainder = sum % 11;
        int digit2 = remainder < 2 ? 0 : 11 - remainder;

        return value[length] - '0' == digit1 && value[length + 1] - '0' == digit2;
    }

    [GeneratedRegex(@"^\d{11}$")]
    private static partial Regex CpfRegex();

    [GeneratedRegex(@"^\d{14}$")]
    private static partial Regex CnpjRegex();
    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailValidationRegex();
}

public class Result<T> : Result where T : notnull
{
    private readonly T? _value;

    protected internal Result(T value) => _value = value;
    protected internal Result(Error error) : base(error) { }

    public T Value =>
        IsOk
        ? _value!
        : throw new InvalidOperationException("Cannot get value from fail result.");

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator T(Result<T> result) => result.Value;
}


