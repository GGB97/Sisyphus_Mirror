using UnityEngine;

public class TutorialManager : SingletoneBase<TutorialManager>
{
    [SerializeField] GameObject _tutorialUI;

    [Header("Tutorial Flag")]
    public int runestoneTutorialFlag;
    public int inventoryTutorialFlag;   // 추후에 엑셀에 저장을 하던 해서 수정하기
    public int dungeonStartTutorialFlag;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("runestoneTutorialFlag"))
        {
            runestoneTutorialFlag = PlayerPrefs.GetInt("runestoneTutorialFlag");
        }
        else runestoneTutorialFlag = 0;

        if (PlayerPrefs.HasKey("inventoryTutorialFlag"))
        {
            inventoryTutorialFlag = PlayerPrefs.GetInt("inventoryTutorialFlag");
        }
        else inventoryTutorialFlag = 0;

        if (PlayerPrefs.HasKey("dungeonStartTutorialFlag"))
        {
            dungeonStartTutorialFlag = PlayerPrefs.GetInt("dungeonStartTutorialFlag");
            Debug.Log(dungeonStartTutorialFlag);
        }
        else dungeonStartTutorialFlag = 0;
    }

    public void PopupTutorial(TutorialType type, int id)
    {
        if (_tutorialUI == null)
            _tutorialUI = Instantiate(Resources.Load("Tutorials/Prefabs/_Tutorials") as GameObject);
        _tutorialUI.GetComponent<TutorialPopup>().SetTutorialPopup(id);

        switch (type)
        {
            case TutorialType.RuneStone:
                runestoneTutorialFlag = 1;
                // TODO : PlayerPrefs에 반영시키기
                PlayerPrefs.SetInt("runestoneTutorialFlag", runestoneTutorialFlag);
                break;
            case TutorialType.Inventory:
                inventoryTutorialFlag = 1;
                // TODO : PlayerPrefs에 반영시키기
                PlayerPrefs.SetInt("inventoryTutorialFlag", inventoryTutorialFlag);
                break;
            case TutorialType.DungeonStart:
                dungeonStartTutorialFlag = 1;
                // TODO : PlayerPrefs에 반영시키기
                PlayerPrefs.SetInt("dungeonStartTutorialFlag", dungeonStartTutorialFlag);
                break;
        }
    }
}
