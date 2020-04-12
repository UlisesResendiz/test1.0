using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    static AudioPlayer z_AudioPlayer;

    public static AudioPlayer GetAudioPlayer()
    {
        return z_AudioPlayer;
    }
    
    private void Awake()
    {
        if (GetAudioPlayer() == null)
        {
            z_AudioPlayer = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    AudioSource a_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        a_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioClipOneShoot(AudioClip clip)
    {
        a_AudioSource.PlayOneShot(clip);
    }
}
