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

    public Vector3 ReturnRandomPosition()
    {
        float x = transform.position.x + Random.Range(-1, 1f);
        float z = transform.position.z + Random.Range(-1, 1f);

        return new Vector3(x, transform.position.y, z);
    }
}
