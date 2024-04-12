using System.Collections;
using UnityEngine;

public class AudioSourceObject : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;  //재생할 클립
    public float volume;//볼륨
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        StartCoroutine("PlayAndRetrun");
    }

    public IEnumerator PlayAndRetrun()
    {
        audioSource.PlayOneShot(clip,volume);

        yield return new WaitForSeconds(clip.length);
        Init();
        SoundManager.Instance.ReturnAudioClip(this.gameObject);
    }
    public void Init()
    {
        clip = null;
        volume = 0;
    }
}