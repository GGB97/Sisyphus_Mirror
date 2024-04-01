using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class Utilities
{
    public static void AddText(StringBuilder sb, string name, float value)
    {
        sb.Append($"{name} : ");
        sb.Append(ChangeColorWithValue(value));
    }
    public static string ChangeColorWithValue(float value)
    {
        string str;
        if (value > 0)
        {
            str = string.Format($"<color=green>{value}</color>\n");
        }
        else
        {
            str = string.Format($"<color=red>{value}</color>\n");
        }
        return str;
    }
    //public static string SetStringColor(Color newColor, string str)
    //{

    //}
    public static Color HexColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }

        return Color.white;
    }
}
