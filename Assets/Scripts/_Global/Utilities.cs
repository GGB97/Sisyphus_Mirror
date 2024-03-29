using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Utilities
{
    public static void AddText(StringBuilder sb, string name, float value)
    {
        sb.Append($"{name} : ");
        sb.Append(ChangeColor(value));
    }
    public static string ChangeColor(float value)
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
}
