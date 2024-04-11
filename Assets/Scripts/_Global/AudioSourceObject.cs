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
        audioSource.PlayOneShot(clip);
    }

    public IEnumerator PlayAndRetrun()
    {
        audioSource.PlayOneShot(clip,volume);

        yield return new WaitForSeconds(clip.length);

        yield return null;
    }
}