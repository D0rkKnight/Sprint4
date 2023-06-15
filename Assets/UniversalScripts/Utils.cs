using System;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class Utils
{
    public static string IncrementSubscript(string input)
    {
        // Use regular expression to match the number at the end of the string
        Match match = Regex.Match(input, @"\d+$");

        if (match.Success)
        {
            string numberString = match.Value;
            int number = int.Parse(numberString);

            // Remove the number from the original string
            string trimmedString = input.Substring(0, input.Length - numberString.Length);

            // Increment the number
            int newNumber = number + 1;

            // Append the incremented number to the trimmed string
            string result = trimmedString + newNumber;

            return result;
        }
        else
        {
            // If no number found, return the original string
            return input;
        }
    }



    public static Vector3 SnapVec3(Vector3 vec, float snapInc)
    {
        vec.x = (float)Math.Round(vec.x);
        vec.y = (float)Math.Round(vec.y);
        vec.z = (float)Math.Round(vec.z);

        return vec;
    }
}
