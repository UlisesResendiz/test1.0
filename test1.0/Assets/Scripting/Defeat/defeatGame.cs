using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defeatGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            //aqui ponemos mas codigo para cuando pierde el jugador
            Quit();
        }

    }
     public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
