using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestClearUI : MonoBehaviour
{
    Vector2 endPosition;//시작 위치
    Vector2 startPosition;
    private void Awake()
    {
        //startPosition = transform.position;
        startPosition = new Vector2(1895f, -275f);//객체를 생성하고 파괴할지 하나의 객체를 사용해서 설명과 아이콘만 바꾸는 형식으로 사용할지 고민
        endPosition = new Vector2(transform.position.x, 25f);
    }
    void Start()
    {
        StartCoroutine("OpenUi");
    }

    public QuestClearUI()
    {
        startPosition = new Vector2(1895f, -275f);
        endPosition = new Vector2(startPosition.x, 25f);

        //설명 적기
    }
    public IEnumerator OpenUi()
    {
        transform.DOMove(endPosition, 1.5f);

        yield return new WaitForSeconds(4f);

        yield return StartCoroutine("CloseUi");
    }
    public IEnumerator CloseUi() 
    {
        transform.DOMove(startPosition, 1.5f);

        yield return null;
    }
}
