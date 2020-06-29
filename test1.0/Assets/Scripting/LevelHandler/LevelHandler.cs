using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{

    [SerializeField]
    AudioClip Audio_Theme1;

    // Start is called before the first frame update
    void Start()
    {
        StartUpTime();
        PlayTheme();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void StartUpTime()
    {
        StartCoroutine(TimerWait(.7f));
    }

    IEnumerator TimerWait(float Duration)
    {
        yield return new WaitForSecondsRealtime(Duration);

        Time.timeScale = 1;
    }

    void PlayTheme()
    {
        AudioPlayer.GetAudioPlayer().GetThemeClip(Audio_Theme1);
    }
}
