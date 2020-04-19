using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    [SerializeField]
    GameObject c_PauseMenu;
    [SerializeField]
    GameObject c_TransitionPanel;
    [SerializeField]
    GameObject c_LosePanel;
    [SerializeField]
    Text c_LosePanelScore;
    [SerializeField]
    Text c_HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        c_HighScoreText.text = PlayerPrefs.GetInt("Highscore1", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRightButton()
    {
        if (BallPhysics.a_BallPhysics)
        {
            BallPhysics.a_BallPhysics.SetMovDir(1);
        }
    }

    public void MoveLeftButton()
    {
        if (BallPhysics.a_BallPhysics)
        {
            BallPhysics.a_BallPhysics.SetMovDir(-1);
        }
    }

    public void ExitLeftRightMov()
    {
        if (BallPhysics.a_BallPhysics)
        {
            BallPhysics.a_BallPhysics.SetMovDir(0);
        }
    }

    public void JumpButton()
    {
        if (BallPhysics.a_BallPhysics)
        {
            BallPhysics.a_BallPhysics.Jump();
        }
    }

    public void ChangetoScene(string SceneName)
    {
        StartCoroutine(ChangeScene(SceneName));
    }

    public void Lose(int Score)
    {
        Time.timeScale = 0;
        PlayLoseTheme();
        c_LosePanelScore.text = Score.ToString();
        saveScores(Score);
        c_LosePanel.GetComponent<Animator>().Play("Anim_LoseImageEnter");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    //Esta va en la de UIHandler
    [SerializeField]
    Text c_ScoreText;
    [SerializeField]
    Text c_HealthText;

    public void ActualizeScoreText(int newScore)
    {
        if (c_ScoreText)
        {
            c_ScoreText.text = newScore.ToString();
        }
        else
        {
            Debug.LogError("El gameobject Score Text no ha sido asignado en el inspector");
        }
    }

    public void EnablePauseMode(bool Enable)
    {
        if (Enable)
        {
            Time.timeScale = 0;
            c_PauseMenu.GetComponent<Animator>().Play("Anim_PauseMenuEnter");
        }
        else
        {
            c_PauseMenu.GetComponent<Animator>().Play("Anim_PauseMenuExit");
            StartCoroutine(ResumeTimer(1));
        }

        
    }


    public void ActualizeHealthText(int newHealth)
    {
        if (c_HealthText)
        {
            c_HealthText.text = newHealth.ToString();
        }
        else
        {
            Debug.LogError("El gameobject Health Text no ha sido asignado en el inspector");
        }
    }

    public void PlayAudio(AudioSource audiosource)
    {
        AudioPlayer player = AudioPlayer.GetAudioPlayer();
        player.PlayAudioClipOneShoot(audiosource.clip);
    }

    public void TransitionEnterPanel()
    {
        if (c_TransitionPanel)
        {
            c_TransitionPanel.GetComponent<Animator>().Play("Anim_TransitionImageEnter");
        }else
        {
            Debug.LogError("El gameobject Transition Panel no ha sido asignado en el inspector");
        }
    }

    IEnumerator ChangeScene(string SceneName)
    {
        TransitionEnterPanel();
        yield return new WaitForSecondsRealtime(1);
        Scene scene = SceneManager.GetSceneByName(SceneName);
        SceneManager.LoadScene(SceneName);
    }


    [Header("Audio")]
    [SerializeField]
    AudioClip Audio_IntroLose;
    [SerializeField]
    AudioClip Audio_ThemeLose;

    void PlayLoseTheme()
    {
        AudioPlayer audplayer = AudioPlayer.GetAudioPlayer();

        audplayer.ProgresiveAudioChange(Audio_IntroLose, Audio_ThemeLose);
    }
    void saveScores(int Score)
    {
        if (Score > PlayerPrefs.GetInt("Highscore1", 0))
        {
            int aux1 = PlayerPrefs.GetInt("Highscore1", 0);
            int aux2 = PlayerPrefs.GetInt("Highscore2", 0);
            int aux3 = PlayerPrefs.GetInt("Highscore3", 0);
            int aux4 = PlayerPrefs.GetInt("Highscore4", 0);
            PlayerPrefs.SetInt("Highscore1", Score);
            c_HighScoreText.text = Score.ToString();
            PlayerPrefs.SetInt("Highscore2", aux1);
            PlayerPrefs.SetInt("Highscore3", aux2);
            PlayerPrefs.SetInt("Highscore4", aux3);
            PlayerPrefs.SetInt("Highscore5", aux4);
        }
        else if (Score > PlayerPrefs.GetInt("Highscore2", 0))
        {
            int aux2 = PlayerPrefs.GetInt("Highscore2", 0);
            int aux3 = PlayerPrefs.GetInt("Highscore3", 0);
            int aux4 = PlayerPrefs.GetInt("Highscore4", 0);
            PlayerPrefs.SetInt("Highscore2", Score);

            PlayerPrefs.SetInt("Highscore3", aux2);
            PlayerPrefs.SetInt("Highscore4", aux3);
            PlayerPrefs.SetInt("Highscore5", aux4);
        }
        else if (Score > PlayerPrefs.GetInt("Highscore3", 0))
        {
            int aux3 = PlayerPrefs.GetInt("Highscore3", 0);
            int aux4 = PlayerPrefs.GetInt("Highscore4", 0);
            PlayerPrefs.SetInt("Highscore3", Score);
            PlayerPrefs.SetInt("Highscore4", aux3);
            PlayerPrefs.SetInt("Highscore5", aux4);
        }
        else if (Score > PlayerPrefs.GetInt("Highscore4", 0))
        {
            int aux4 = PlayerPrefs.GetInt("Highscore4", 0);
            PlayerPrefs.SetInt("Highscore4", Score);
            PlayerPrefs.SetInt("Highscore5", aux4);
        }
        else if (Score > PlayerPrefs.GetInt("Highscore5", 0))
        {
            PlayerPrefs.SetInt("Highscore5", Score);
        }

    IEnumerator ResumeTimer(float Duration)
    {
        yield return new WaitForSecondsRealtime(Duration);
        Time.timeScale = 1;
    }
}
