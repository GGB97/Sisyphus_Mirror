using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]  //해당 컴포넌트를 자동으로 추가해줌
[RequireComponent(typeof(ScrollRect))]

public class UIPagingViewController : UI_Base, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] protected GameObject gbj_ContentRoot = null;

    [SerializeField] protected UIPageControl pageControl;

    [SerializeField] private float animationDuration = 0.3f;

    private float key1InTangent = 0f;
    private float key1OutTangent = 1f;
    private float key2InTangent = 1f;
    private float key2OutTangent = 0f;

    private bool isAnimating = false;         // 애니메이션 재생 중임을 나타내는 
    private Vector2 destPosition;             // 최종적인 스크롤 위치
    private Vector2 initialPosition;          // 자동 스크롤을 시작할 때의 스크롤 위치
    private AnimationCurve animationCurve;    // 자동 스크롤에 관련된 애니메이션 커브
    public int prevPageIndex = 0;            // 이전 페이지의 인덱스
    private Rect currentViewRect;             // 스크롤 뷰의 사각형 크기


    public RectTransform CachedRectTransform
    {
        get { return GetComponent<RectTransform>(); }
    }

    public ScrollRect CachedScrollRect
    {
        get { return GetComponent<ScrollRect>(); }
    }

    public void OnBeginDrag(PointerEventData eventData)         // 드래그가 시작될 때 호출
    {
        isAnimating = false;      // 애니메이션 도중에 플래그 리셋
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GridLayoutGroup grid = CachedScrollRect.content.GetComponent<GridLayoutGroup>();

        CachedScrollRect.StopMovement();     // 현재 동작 중인 스크롤 뷰를 멈춘다

        float pageWidth = -(grid.cellSize.x + grid.spacing.x);  // 페이지의 폭을 계산

        int pageIndex = Mathf.RoundToInt((CachedScrollRect.content.anchoredPosition.x) / pageWidth);   // 스크롤의 현재 위치로부터 맞출 페이지의 인덱스를 계산

        if (pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 4)
        {
            // 일정 속도 이상으로 드래그할 경우 해당 방향으로 한 페이지 진행
            CachedScrollRect.content.anchoredPosition += new Vector2(eventData.delta.x, 0.0f);
            pageIndex += (int)Mathf.Sign(-eventData.delta.x);
        }

        // 첫 페이지 또는 마지막 페이지일 경우 그 이상 스크롤하지 않도록
        if (pageIndex < 0)
        {
            pageIndex = 0;
        }
        else if (pageIndex > grid.transform.childCount - 1)
        {
            pageIndex = grid.transform.childCount - 1;
        }

        prevPageIndex = pageIndex;    //현재 페이지의 인덱스를 유지

        float destX = pageIndex * pageWidth;  // 최종적인 스크롤 위치를 계산
        destPosition = new Vector2(destX, CachedScrollRect.content.anchoredPosition.y);

        initialPosition = CachedScrollRect.content.anchoredPosition; //시작할 때의 스크롤 위치

        // 애니메이션 커브 작성
        Keyframe keyFrame1 = new Keyframe(Time.time, 0.0f, key1InTangent, key1OutTangent);
        Keyframe keyFrame2 = new Keyframe(Time.time + animationDuration, 1.0f, key2InTangent, key2OutTangent);
        animationCurve = new AnimationCurve(keyFrame1, keyFrame2);

        isAnimating = true; // 애니메이션 재생 중을 나타내는 플래그

        if (pageControl != null) // 페이지 컨트롤 표시를 갱신
        {
            pageControl.SetCurrentPage(pageIndex);
        }
    }

    private void LateUpdate()    // 매 프래임마다 update 메서드가 처리된 다음 호출
    {
        if (isAnimating)
        {
            if (Time.time >= animationCurve.keys[animationCurve.length - 1].time)
            {
                // 애니메이션 커브의 마지막 키프레임을 지나가면 애니메이션 종료
                CachedScrollRect.content.anchoredPosition = destPosition;
                isAnimating = false;
                return;
            }

            // 애니메이션 커브를 사용하여 현재 스크롤 위치를 계산해서 스크롤 뷰를 이동
            Vector2 newPosition = initialPosition + (destPosition - initialPosition) * animationCurve.Evaluate(Time.time);
            CachedScrollRect.content.anchoredPosition = newPosition;
        }
    }

    private void Start()
    {
        UpdateView();

        if (pageControl != null)
        {
            if (gbj_ContentRoot != null)
                pageControl.SetNumberOfPages(gbj_ContentRoot.transform.childCount);
            pageControl.SetCurrentPage(0);     // 페이지 컨트롤 표시를 초기화
        }
    }

    private void Update()
    {
        if (CachedRectTransform.rect.width != currentViewRect.width || CachedRectTransform.rect.height != currentViewRect.height)
        {
            // 스크롤 뷰의 폭이나 높이가 변화하면 Scroll Content의 Padding을 갱신
            UpdateView();
        }
    }


    private void UpdateView()  //Scroll Content의 Padding을 갱신하는 메서드
    {
        currentViewRect = CachedRectTransform.rect; // 스크롤 뷰의 사각형 크기를 보존

        // GridLayoutGroup의 cellSize를 사용하여 Scroll Content의 Padding을 계산하여 설정
        GridLayoutGroup grid = CachedScrollRect.content.GetComponent<GridLayoutGroup>();
        int paddingH = Mathf.RoundToInt((currentViewRect.width - grid.cellSize.x) / 2f);
        int paddingV = Mathf.RoundToInt((currentViewRect.height - grid.cellSize.y) / 2f);
        grid.padding = new RectOffset(paddingH, paddingH, paddingV, paddingV);
    }

    public void SelectButton()
    {
        //  Debug.Log(prevPageIndex);
        PlayerManager.Instance.ChangePlayer(prevPageIndex);
        gameObject.SetActive(false);
    }

    public void ExitUI()
    {
        gameObject.SetActive(false);
    }
}
