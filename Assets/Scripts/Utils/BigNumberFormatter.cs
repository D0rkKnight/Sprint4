using System;

public static class BigNumberFormatter
{
    private static readonly string[] NumberSuffixes = { "", "k", "m", "b", "t", "qd", "qt", "qn", "sx", "sp", "o", "n" };
    
    public static string Format(double number)
    {
        const double step = 1000;

        if (Math.Abs(number) < step)
            return FormatNumerical(number);

        int suffixIndex = 0;
        while (Math.Abs(number) >= step && suffixIndex < NumberSuffixes.Length - 1)
        {
            number /= step;
            suffixIndex++;
        }

        return $"{FormatNumerical(number)}{NumberSuffixes[suffixIndex]}";

        static string FormatNumerical(double number)
        {
            return $"{number:0.#}";
        }
    }
}
