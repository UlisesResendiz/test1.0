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
    AudioClip a_ThemeAudio;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        PlayTheme();
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

    public void GetThemeClip(AudioClip clip)
    {
        a_ThemeAudio = clip;

        if (a_AudioSource != null)
        {
            PlayTheme();
        }
    }

    void PlayTheme()
    {
        a_AudioSource.clip = a_ThemeAudio;
        a_AudioSource.volume = PlayerPrefs.GetFloat("volumen", 0.0f);
        a_AudioSource.Play();
    }

    public void ProgresiveAudioChange(AudioClip clip1, AudioClip clip2)
    {
        /*
        a_AudioSource.loop = false;
        a_AudioSource.clip = clip1;
        a_AudioSource.Play();
        StartCoroutine(Transitionto2ndClip(clip2));*/

        a_AudioSource.clip = clip2;
        a_AudioSource.Play();
    }

    IEnumerator Transitionto2ndClip(AudioClip clip2)
    {

        while (a_AudioSource.isPlaying)
        {
            yield return new WaitForSecondsRealtime(.01f);
        }
        a_AudioSource.loop = true;
        a_AudioSource.clip = clip2;
        a_AudioSource.Play();

    }


}
