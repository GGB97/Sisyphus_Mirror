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

    public void TutorialPopup(int id)
    {
        TutorialPopup tutorial;
        if(tutorialFlag == 0)
        {
            tutorial = Instantiate(_tutorialUI).GetComponent<TutorialPopup>();
            tutorial.SetTutorialPopup(id);
        }
    }
}
