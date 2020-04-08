﻿using System.Collections;
using System.Collections.Generic;
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

    public void RedButton()
    {
        if (BallPhysics.a_BallPhysics)
        {
            BallPhysics.a_BallPhysics.SetMovDir(1);
        }
    }

    public void BlueButton()
    {
        if (BallPhysics.a_BallPhysics)
        {
            BallPhysics.a_BallPhysics.SetMovDir(-1);
        }
    }

    public void ExitRedBlueButton()
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
