using System;
using System.Text.RegularExpressions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public partial record Cnpj
{
    public const int Length = 14;
    private Cnpj(string value) => Value = value;
    public string Value { get; }

    public static Result<Cnpj> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Cnpj>(Error.InvalidInput("CNPJ must be provided."));
        }

        value = Regex().Replace(value, string.Empty);

        if (value.Length != Length)
        {
            return Result.Fail<Cnpj>(Error.InvalidInput("CNPJ must be 14 digits long."));
        }

        if (!IsValid(value))
        {
            return Result.Fail<Cnpj>(Error.InvalidInput("Invalid CNPJ."));
        }

        return Result.Ok(new Cnpj(value));
    }

    public static bool IsValid(string cnpj)
    {
        if (cnpj.Length != Length || new string(cnpj[0], Length) == cnpj)
        {
            return false;
        }

        int[] multipliers1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multipliers2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int sum = 0;

        for (int i = 0; i < 12; i++)
        {
            sum += int.Parse(tempCnpj[i].ToString()) * multipliers1[i];
        }

        int remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;

        string digit = remainder.ToString();
        tempCnpj += digit;
        sum = 0;

        for (int i = 0; i < 13; i++)
        {
            sum += int.Parse(tempCnpj[i].ToString()) * multipliers2[i];
        }

        remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;

        digit += remainder.ToString();

        return cnpj.EndsWith(digit);
    }

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex Regex();
}
