using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonGate : MonoBehaviour
{

    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/Fade_Canvas"));
            Image image = go.GetComponentInChildren<Image>();

            image.DOFade(1, 0.5f).OnComplete(LoadScene);
        }
    }
}
