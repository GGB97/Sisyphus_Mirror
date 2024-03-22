using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    private void Start()
    {
        DungeonManager.Instance.SetStageAndStart();
        Destroy(this.gameObject);
    }
}
