using UnityEngine;
using UnityEngine.UI;

public class TutorialScrollBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Scrollbar>().value = 1;
    }
}
