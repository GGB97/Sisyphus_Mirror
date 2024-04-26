using DG.Tweening;
using KoreanTyper;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image backGround;
    [SerializeField] Image loadingBar;
    public string[] strings;

    public Action loadSceneAction;

    private void Start()
    {
        backGround.color = Color.black;

        StartCoroutine(TypingText());
    }

    public IEnumerator TypingText()
    {
        WaitForSeconds textDelay = new(0.03f);
        WaitForSeconds oneSec = new(1f);

        for (int t = 0; t < strings.Length; t++)
        {
            text.text = "";
            int strTypingLength = strings[t].GetTypingLength();

            if (t == 3)
            {
                FadeIn(4f);
                yield return oneSec;
            }

            for (int i = 0; i <= strTypingLength; i++)
            {
                text.text = strings[t].Typing(i);
                yield return textDelay;
            }
            // Wait 1 second per 1 sentence | 한 문장마다 1초씩 대기
            yield return oneSec;
        }
        // Wait 1 second at the end | 마지막에 1초 추가 대기함
        yield return oneSec;
        loadSceneAction?.Invoke();
    }

    public void UpdateFill(float fillAmount)
    {
        loadingBar.fillAmount = fillAmount;
    }

    void FadeIn(float time)
    {
        backGround.color = Color.black;

        backGround.DOColor(new(135f / 255f, 135f / 255f, 135f / 255f), time);
    }
}
