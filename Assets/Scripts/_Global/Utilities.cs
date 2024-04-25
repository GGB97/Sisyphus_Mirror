using System.Text;
using UnityEngine;

public static class Utilities
{
    public static void AddText(StringBuilder sb, string name, float value, bool isPercent = false, bool isReverse = false)
    {
        sb.Append($"{name} : ");
        sb.Append(ChangeColorWithValue(value, isPercent, isReverse));
    }
    public static string ChangeColorWithValue(float value, bool isPercent = false, bool isReverse = false)
    {
        string str;
        if (isReverse == true)//값이 리버스 되어야 하는지 ( = 플레이어한테 이로운지 헤로운지)
        {
            if (isPercent == true)//값이 %로 표현되어야 하는지
            {
                if (value > 0)
                {
                    str = string.Format($"<color=red>{value.ToString("F2")}</color> % \n");
                }
                else
                {
                    str = string.Format($"<color=green>{value.ToString("F2")}</color> % \n");
                }
            }
            else
            {
                if (value > 0)
                {
                    str = string.Format($"<color=red>{value}</color>\n");
                }
                else
                {
                    str = string.Format($"<color=green>{value}</color>\n");
                }
            }

        }
        else
        {
            if (isPercent == true)
            {
                if (value > 0)
                {
                    str = string.Format($"<color=green>{value.ToString("F2")}</color> % \n");
                }
                else
                {
                    str = string.Format($"<color=red>{value.ToString("F2")}</color> % \n");
                }
            }
            else
            {
                if (value > 0)
                {
                    str = string.Format($"<color=green>{value}</color>\n");
                }
                else
                {
                    str = string.Format($"<color=red>{value}</color>\n");
                }
            }
        }
        return str;
    }
    public static string SetStringGreen(int value)
    {
        string str;
        str = string.Format($"<color=green>{value}</color>");
        return str;
    }
}
