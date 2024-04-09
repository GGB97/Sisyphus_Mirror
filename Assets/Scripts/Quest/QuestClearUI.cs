using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestClearUI : MonoBehaviour
{
    RectTransform canvasRectTransform;
    Vector2 endPosition;
    Vector2 startPosition;
    private void Awake()
    {
        startPosition = transform.position;
        endPosition = new Vector2(transform.position.x, 25f);
    }
    void Start()
    {
        StartCoroutine("OpenUi");
    }

    public IEnumerator OpenUi()
    {
        transform.DOMove(endPosition, 2f);

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine("CloseUi");
    }
    public IEnumerator CloseUi() 
    {
        transform.DOMove(startPosition, 2f);

        yield return null;
    }
}
