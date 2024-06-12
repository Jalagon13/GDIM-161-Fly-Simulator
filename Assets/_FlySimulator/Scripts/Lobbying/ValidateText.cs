using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ValidateText : MonoBehaviour
{
    //By Alaina Klaes
    //We can add checking for bad words here too eventually, if necessary

    private static char[] allowedLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_".ToCharArray();

    public static string ReturnValidString(string newName)
    {
        string validName = "";
        foreach (char letter in newName)
        {
            if (allowedLetters.Contains(letter))
            {
                validName += letter;
            }
        }
        if (validName.Length > 29) { return validName.Substring(0, 29); }
        return validName;

    }
}
