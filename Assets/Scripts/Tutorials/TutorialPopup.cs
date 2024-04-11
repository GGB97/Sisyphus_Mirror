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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (tutorial != null && tutorial.hasNextPage)
            {
                Debug.Log(tutorial.NextPageId);
                SetTutorialPopup(tutorial.NextPageId);
            }
            else Destroy(gameObject);
            Time.timeScale = 1.0f;
        }
    }

    public void SetTutorialPopup(int id)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;

        tutorial = DataBase.Tutorial.Get(id);
        if (tutorial != null)
        {
            _tutorialImage.sprite = tutorial.TutorialImage;
            _tutorialName.text = tutorial.TutorialName;
            _tutorialText.text = tutorial.TutorialText;
        }
    }
}
