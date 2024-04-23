using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stageText;
    DungeonManager dungeonManager;
    // Start is called before the first frame update
    private void Start()
    {
        dungeonManager = DungeonManager.Instance;
        TextUpdate();
    }
    private void OnEnable()
    {
        if (dungeonManager != null)
        { 
            TextUpdate();
            Debug.Log("스테이지 onEnable");
        }
    }
    public void TextUpdate()
    {
        stageText.text = string.Format($"다음 스테이지 : {dungeonManager.currnetstage +1}");
    }
}
