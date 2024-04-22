using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    
    

    IEnumerator CoolTimeFunc(float cooltime, float cooltimeMax, Image skill)
    {
        while (cooltime > 0f)
        {
            cooltime -= Time.deltaTime;

            skill.fillAmount = cooltime / cooltimeMax;

            yield return new WaitForFixedUpdate();
        }
    }
}
