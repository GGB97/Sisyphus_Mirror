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
        Init();
    }
    private IEnumerator PlayAndRetrun()
    {
        audioSource.PlayOneShot(clip, volume);

        yield return new WaitForSeconds(clip.length);
        Init();
        SoundManager.Instance.ReturnAudioClip(this.gameObject);
        yield return null;
    }
    public void Init()
    {
        clip = null;
        volume = 0;
    }
    public void PlayAudio()
    {
        StartCoroutine("PlayAndRetrun");
    }
}