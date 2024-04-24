using UnityEngine;

public class Sword_1_Epic_Skill : MonoBehaviour
{
    public float duration = 2f;
    float _timeSinceFired;
    public float speed = 15f;
    float _damage = 200;

    public int hitId;
    public GameObject flash;

    public Rigidbody rb;
    public ParticleSystem ps;
    public Collider sc;
    public Light li;

    // Start is called before the first frame update
    void Start()
    {
        hitId = 71000003;
    }

    private void Update()
    {
        if (Time.time - _timeSinceFired >= duration) Disappear();
    }

    public void OnSkillStart()
    {
        _timeSinceFired = Time.time;
        _damage = 198 + (2 * DungeonManager.Instance.currnetstage);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ParticleObjectPool.Instance.ReturnToPull(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Enemy == (1 << other.gameObject.layer | LayerData.Enemy))
        {
            if (other.TryGetComponent<HealthSystem>(out HealthSystem enemy))
            {
                enemy.TakeDamage(_damage, DamageType.Magic);
                Vector3 pos = enemy.transform.position;
                pos.y = 1;
                Particle hit = ParticleObjectPool.Instance.SpawnFromPool(hitId, pos, Quaternion.identity).GetComponent<Particle>();
                hit.PlayerPartcle();
            }
        }
    }
}
