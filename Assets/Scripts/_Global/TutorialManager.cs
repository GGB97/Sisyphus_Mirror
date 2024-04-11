using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : SingletoneBase<TutorialManager>
{
    [SerializeField] GameObject _tutorialUI;
    
    public int tutorialFlag;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("tutorialFlag"))
        {
            tutorialFlag = PlayerPrefs.GetInt("tutorialFlag");
        }
        else tutorialFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopupTutorial(int id)
    {
        if (tutorialFlag == 0)
        {
            Debug.Log("PopupTutorial");
            if (_tutorialUI == null)
                _tutorialUI = Instantiate(Resources.Load("Tutorials/Prefabs/_Tutorials") as GameObject);
            _tutorialUI.GetComponent<TutorialPopup>().SetTutorialPopup(id);
        }
    }
}
