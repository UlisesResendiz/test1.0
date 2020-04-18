using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Text table_score1;
    [SerializeField]
    Text table_score2;
    [SerializeField]
    Text table_score3;
    [SerializeField]
    Text table_score4;
    [SerializeField]
    Text table_score5;
    AudioSource a_AudioSource;
    void Start()
    {

        table_score1.text = PlayerPrefs.GetInt("Highscore1", 0).ToString();
        table_score2.text = PlayerPrefs.GetInt("Highscore2", 0).ToString();
        table_score3.text = PlayerPrefs.GetInt("Highscore3", 0).ToString();
        table_score4.text = PlayerPrefs.GetInt("Highscore4", 0).ToString();
        table_score5.text = PlayerPrefs.GetInt("Highscore5", 0).ToString();

        a_AudioSource = GetComponent<AudioSource>();
    }

   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayAudio(AudioClip clip)
    {
        a_AudioSource.volume = PlayerPrefs.GetFloat("effects", 0.75f);
        a_AudioSource.PlayOneShot(clip);
    }
}
