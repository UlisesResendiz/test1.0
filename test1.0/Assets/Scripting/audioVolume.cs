using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioVolume : MonoBehaviour
{
    private AudioSource audioSrc;

    public float musicVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = musicVolume;
    }
    public void SetVolume(float vol)
    {
        musicVolume = vol;
        PlayerPrefs.SetFloat("volumen", musicVolume);
    }

}
