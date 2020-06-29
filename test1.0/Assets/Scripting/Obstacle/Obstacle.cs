using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    GameObject z_ParticleSystem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayParticles()
    {
        GameObject particles = Instantiate(z_ParticleSystem);
        particles.transform.position = transform.position;
        particles.transform.localScale = transform.localScale;
        particles.transform.rotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
    }
}
