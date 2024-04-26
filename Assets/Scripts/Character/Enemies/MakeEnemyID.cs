using UnityEngine;
using UnityEngine.UI;

public class MakeEnemyID : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }
    public EnemyType type;
    public EnemyRank rank;
    public EnemyElement element;
    public EnemySize size;

    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(MakeID);
    }

    public void MakeID()
    {
        ID = 0;

        ID += 3 * 10000000;
        ID += (int)type * 100000;
        ID += (int)rank * 10000;
        ID += (int)element * 100;
        ID += (int)size * 1;
    }

    public void TimeScale()
    {
        if (Time.timeScale < 1f)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0.2f;
    }
}
