using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    public Transform Player { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.parent = Player;
    }

    public Transform ReturnWeaponContainer()
    {
        return transform;
    }
}
