using UnityEngine;

public class ParticleSFX : MonoBehaviour
{
    //public bool Repeating = true;
    //public float RepeatTime = 2.0f;
    //public float StartTime = 0.0f;
    //public bool RandomVolume;
    //public float minVolume = .4f;
    //public float maxVolume = 1f;

    [SerializeField] string audioClip;
    private AudioClip clip;

    public void PlaySFX(string sfxTag)
    {
        audioClip = sfxTag;
        SoundManager.Instance.PlayAudioClip(audioClip);
    }
}
