using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletoneBase<UIManager>
{
    Image _fadeImg;
    Image FadeImg
    {
        get
        {
            if (_fadeImg == null)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("UI/Fade_Canvas"));
                _fadeImg = go.GetComponentInChildren<Image>();

                DontDestroyOnLoad(go);
            }

            return _fadeImg;
        }
    }


    public void FadeIn(float time, Action action = null) // 암전 -> 이미지
    {
        FadeImg.color = new Color(0, 0, 0, 1);
        FadeImg.DOFade(0, time).OnComplete(() =>
        {
            action?.Invoke();
        });
    }

    public void FadeOut(float time, Action action = null) // 이미지 -> 암전
    {
        FadeImg.color = new Color(0, 0, 0, 0);
        FadeImg.DOFade(1, time).OnComplete(() =>
        {
            action?.Invoke();
        });
    }
}
