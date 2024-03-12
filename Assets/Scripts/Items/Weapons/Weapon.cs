using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Transform> Target = new List<Transform>();
    public int id;
    public WeaponData weaponData;

    // Start is called before the first frame update
    void Start()
    {
        weaponData = DataBase.Weapon.Get(id);
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemyInRange();
    }

    public void DetectEnemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, weaponData.Range, 1 << 7);

        foreach (Collider collider in colliders)
        {
            Debug.Log($"Detect : {collider.name}");
            Target.Add(collider.transform);
            Debug.Log(Target.Count);
        }
    }
}
