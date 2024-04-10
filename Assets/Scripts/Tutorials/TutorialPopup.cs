using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] Image _tutorialImage;
    [SerializeField] TextMeshProUGUI _tutorialName;
    [SerializeField] TextMeshProUGUI _tutorialText;
    TutorialData tutorial;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (tutorial != null && tutorial.hasNextPage)
            {
                SetTutorialPopup(tutorial.NextPageId);
            }
            else Destroy(gameObject);
        }
    }

    public void SetTutorialPopup(int id)
    {
        tutorial = DataBase.Tutorial.Get(id);
        if (tutorial != null)
        {
            _tutorialImage = tutorial.TutorialImage;
            _tutorialName.text = tutorial.TutorialName;
            _tutorialText.text = tutorial.TutorialText;
        }
    }
}
