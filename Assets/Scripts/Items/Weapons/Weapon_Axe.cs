using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Axe : MonoBehaviour
{
    [SerializeField] string _sfxTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerSFX()
    {
        SoundManager.Instance.PlayAudioClip(_sfxTag);
    }
}
