using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = false;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    [SerializeField] ParticleSFX _skillSFX;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            GameObject EffectsOnCollision = ParticleObjectPool.Instance.SpawnFromPool(71000004, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion());

            //var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
            if (!UseWorldSpacePosition) EffectsOnCollision.transform.parent = transform;
            if (UseFirePointRotation) { EffectsOnCollision.transform.LookAt(transform.position); }
            else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { EffectsOnCollision.transform.rotation = Quaternion.Euler(rotationOffset); }
            else
            {
                EffectsOnCollision.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                EffectsOnCollision.transform.rotation *= Quaternion.Euler(rotationOffset);
            }
            //Destroy(instance, DestroyTimeDelay);
            
            _skillSFX.PlaySFX("Magic_Hit");
            EffectsOnCollision.GetComponent<Particle>().PlayerPartcle();
        }
        if (DestoyMainEffect == true)
        {
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }
}
