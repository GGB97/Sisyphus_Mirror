using UnityEngine;

public class TutorialBoard : MonoBehaviour
{
    [SerializeField] GameObject _tutorialBoardCanvas;

    public void PopupTutorialBoard()
    {
        _tutorialBoardCanvas.SetActive(true);
    }

    public void OnClickLobbyTutorialButton()
    {
        TutorialManager.Instance.PopupTutorial(TutorialType.DungeonStart, 60003011);
    }

    public void OnClickDungeonTutorialButton()
    {
        TutorialManager.Instance.PopupTutorial(TutorialType.DungeonStart, 60002011);
    }

    public void OnClickForgeTutorialButton()
    {
        TutorialManager.Instance.PopupTutorial(TutorialType.DungeonStart, 60001011);
    }

    public void OnClickCloseButton()
    {
        _tutorialBoardCanvas.SetActive(false);
    }
}
