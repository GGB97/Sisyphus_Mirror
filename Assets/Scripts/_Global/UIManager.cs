using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletoneBase<UIManager>
{
    Dictionary<string, GameObject> _ui = new();

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

    public void AddActiveUI(GameObject value)
    {
        string key = value.name;
        if (_ui.ContainsKey(key) == false)
        {
            _ui.Add(key, value);
        }
    }

    public void RemoveActiveUI(GameObject obj)
    {
        string key = obj.name;
        if (_ui.ContainsKey(key))
        {
            _ui.Remove(key);
        }
    }

    public bool CheckActiveUI()
    {
        if (_ui.Count != 0)
        {
            var ui = _ui.Last().Value.GetComponent<UI_Base>();

            ui.CloseUI();
            _ui.Remove(ui.gameObject.name);

            return false;
        }
        else
        {
            return true;
        }
    }
}
