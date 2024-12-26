using System;
using System.Text.RegularExpressions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public partial record Cpf
{
    public const int Length = 11;
    private Cpf(string value) => Value = value;
    public string Value { get; }

    public static Result<Cpf> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Cpf>(Error.InvalidInput("CPF must be provided."));
        }

        value = Regex().Replace(value, string.Empty);

        if (value.Length != Length)
        {
            return Result.Fail<Cpf>(Error.InvalidInput("CPF must be 11 digits long."));
        }

        if (!IsValid(value))
        {
            return Result.Fail<Cpf>(Error.InvalidInput("Invalid CPF."));
        }

        return Result.Ok(new Cpf(value));
    }

    public static bool IsValid(string cpf)
    {
        if (cpf.Length != Length || new string(cpf[0], Length) == cpf)
        {
            return false;
        }

        int[] multipliers1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multipliers2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(tempCpf[i].ToString()) * multipliers1[i];
        }

        int remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;

        string digit = remainder.ToString();
        tempCpf += digit;
        sum = 0;

        for (int i = 0; i < 10; i++)
        {
            sum += int.Parse(tempCpf[i].ToString()) * multipliers2[i];
        }

        remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;

        digit += remainder.ToString();

        return cpf.EndsWith(digit);
    }

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex Regex();
}
