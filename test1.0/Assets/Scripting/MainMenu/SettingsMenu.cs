using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    float volumenMusic;
    float volumenEffects;
    // Start is called before the first frame update
    void Start()
    {
        getVolumen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void getVolumen()
    {
        volumenMusic = PlayerPrefs.GetFloat("volumen", 0.75f);
        volumenEffects = PlayerPrefs.GetFloat("effects", 0.75f);

        GameObject.Find("value_music").GetComponent<Slider>().value = volumenMusic;
        GameObject.Find("value_effects").GetComponent<Slider>().value = volumenEffects;
    }
}
