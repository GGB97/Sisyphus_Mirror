using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestClearUI : MonoBehaviour
{
    QuestManager _questManager;

    Vector2 endPosition;//시작 위치
    Vector2 startPosition;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    Queue<QuestData> achievementQueue = new Queue<QuestData>();
    private bool isAnimating = false;//ui가 보여지는 중인지
    private IEnumerator currentCoroutine;//현재 실행 중인 코루틴

    private void Awake()
    {
        //startPosition = transform.position;
        startPosition = new Vector2(1895f, -275f);//객체를 생성하고 파괴할지 하나의 객체를 사용해서 설명과 아이콘만 바꾸는 형식으로 사용할지 고민
        endPosition = new Vector2(transform.position.x, 25f);
    }
    void Start()
    {
        _questManager = QuestManager.Instance;
        _questManager.OnQuestClearCallback += AddClearQuestData;
    }

    private void ShowAchievementUI()//UI 보이기
    {
        StartCoroutine("OpenUi");
    }
    public void AddClearQuestData(int questId)
    {
        QuestData questData = DataBase.Quest.Get(questId);
        achievementQueue.Enqueue(questData); // 업적을 큐에 추가

        if (!isAnimating) // 애니메이션이 진행 중이 아니라면
        {
            StartCoroutine(ShowAllAchievement()); // 업적을 표시하는 코루틴 시작
        }
    }
    public void SetUIDescription(QuestData questData)
    {
        nameText.text = questData.Name;
        descriptionText.text = questData.Description;
    }
    public IEnumerator ShowAllAchievement()
    {
        isAnimating = true;

        while (achievementQueue.Count > 0)
        {
            QuestData questData = achievementQueue.Dequeue(); // 큐에서 업적을 가져옴

            // UI에 업적을 표시하고 애니메이션 진행
            SetUIDescription(questData);
            ShowAchievementUI();
            yield return new WaitForSeconds(4.7f);
        }

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

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine("CloseUi");
    }
    public IEnumerator CloseUi()
    {
        transform.DOMove(startPosition, 1.5f);

        yield return new WaitForSeconds(1.6f);
        isAnimating = false;
    }

    private void OnDestroy()
    {
        _questManager.OnQuestClearCallback -= AddClearQuestData;
    }
}
