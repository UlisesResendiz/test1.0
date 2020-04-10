using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    [SerializeField]
    GameObject c_PauseMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        
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

    //Esta va en la de UIHandler
    [SerializeField]
    Text c_ScoreText;

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
        Time.timeScale = 0;
        if (Enable)
        {
            c_PauseMenu.GetComponent<Animator>().Play("Anim_PauseMenuEnter");
        }
        else
        {
            c_PauseMenu.GetComponent<Animator>().Play("Anim_PauseMenuExit");
        }
    }
}
