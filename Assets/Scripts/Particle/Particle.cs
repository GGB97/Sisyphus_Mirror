using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;

    public void PlayerPartcle()
    {
        _particle.Play();
        Invoke("DisableParticle", .5f);
    }

    void DisableParticle()
    {
        _particle.Stop();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ParticleObjectPool.Instance.ReturnToPull(gameObject);
    }
}
