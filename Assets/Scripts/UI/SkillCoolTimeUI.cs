using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    public static SkillCoolTimeUI Instance;
    public Image skill;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else return;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    IEnumerator CoolTimeFunc(float cooltime, float cooltimeMax)
    {
        WaitForFixedUpdate waitFixed = new();
        yield return null;
        while (cooltime > 0f)
        {
            cooltime -= Time.deltaTime;

            skill.fillAmount = cooltime / cooltimeMax;

            yield return waitFixed;
        }
        gameObject.SetActive(false);
    }

    public void TestSkill(float coolTime, float coolTimeMax)
    {
        gameObject.SetActive(true);
        StartCoroutine(CoolTimeFunc(coolTime, coolTimeMax));
    }
}
