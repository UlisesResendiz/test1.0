using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class BallPhysics : MonoBehaviour
{
    public static BallPhysics a_BallPhysics;

    Rigidbody2D a_rb;
    AudioSource a_AudioSource;

    [System.Serializable]
    public struct BallProps
    {
        public float RotationVel;
        public float JumpForce;
    }

    [SerializeField]
    BallProps a_BallProps;
    bool a_CanJump;
    int a_DirMov;

    private int currentScore;

    private int currentHealth;

    public GameObject floor;
    private RingBehaviour rg_script;
    Vector3 temp = new Vector3(0, 0, 0);
  
    [SerializeField]
    UIHandler c_UI;   //Referencia al script del UI

    [Header("Audio")]
    [SerializeField]
    AudioClip Audio_ObstacleHit;
    [SerializeField]
    AudioClip Audio_RingCrossed;
    [SerializeField]
    AudioClip Audio_Jump;

    float timeLeft = 15.0f;
    bool lose = false;
    public ParticleSystem particleSystem;
    private bool isPlaying = false;

    private void Awake()
    {
        a_BallPhysics = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        currentScore = 0;

        currentHealth = 2;
        ActualizeScore();
        ActualizeHealth();


        rg_script = GameObject.Find("FloorRing").GetComponent<RingBehaviour>();

        a_rb = GetComponent<Rigidbody2D>();
        a_AudioSource = GetComponent<AudioSource>();

        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Clear();
        particleSystem.Pause();
    }

    // Update is called once per frame
    void Update()
    {

        Rotation();
        MobileMovement();
        temp = rg_script.temp;
        ScalingBall();

        if (gameObject.GetComponent<ParticleSystem>().isStopped)
        {
            c_UI.Lose(currentScore);
        }
        if (!gameObject.GetComponent<ParticleSystem>().isPlaying)
        {
            KeyMapping();
        }
    


    }

    void SetUp()
    {
        a_CanJump = false;
    }

    private void FixedUpdate()
    {
        Physics2D.gravity = new Vector3(0, 0, 0) - transform.position;
    }

    void KeyMapping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(transform.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(-transform.right);
        }
    }



    void Rotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, AngleBetweenVector2(new Vector2(transform.position.x, transform.position.y), new Vector2(0, 0)));
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign + 90;
    }

    public void Move(Vector3 Direction)
    {
        //modificar para la velocidad segun la escala del aro
        if (temp.x > -2f && temp.x < .5f)
        {
            transform.position = transform.position + Direction * (a_BallProps.RotationVel * 2) * Time.deltaTime;
        }
        else if (temp.x > .5 && temp.x < 1.25f)
        {
            transform.position = transform.position + Direction * (a_BallProps.RotationVel * 3) * Time.deltaTime;
        }
        else if (temp.x > 1.25f && temp.x <2)
        {   
        transform.position = transform.position + Direction * (a_BallProps.RotationVel * 5) * Time.deltaTime;
        }
        else if(temp.x > 2)
        {
            transform.position = transform.position + Direction * (a_BallProps.RotationVel * 7) * Time.deltaTime;
        }
    }

    void MobileMovement()
    {
        if (a_DirMov == 1)
        {
            Move(transform.right);
        }
        else if (a_DirMov == -1)
        {
            Move(-transform.right);
        }
    }

    public void SetMovDir(int value)
    {
        a_DirMov = value;
    }

    public void Jump()
    {
        if (a_CanJump)
        {
            float DisttoOrigin = Vector3.Distance(Vector3.zero, transform.position);

            float newforce = DisttoOrigin * 2;

            a_rb.AddForce(transform.up * newforce, ForceMode2D.Impulse);
          
            PlayAudio(Audio_Jump);
           
        }
    }

    void ScalingBall()
    {
        //Calcula la distancia de vector(0,0,0) a la distancia que tiene la pelota
        //Mientras mas distancia mas grande la escala de la pelota o al contrario
        //La escala maxima sera 1.5


        float DisttoOrigin = Vector3.Distance(Vector3.zero, transform.position);

        //Debug.Log(DisttoOrigin);

        float newscale = DisttoOrigin / 13;

        if (newscale < 1.5f)
        {
            transform.localScale = new Vector3(newscale, newscale, 1);
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        a_CanJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        a_CanJump = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            currentScore++;
            ActualizeScore();
            PlayAudio(Audio_RingCrossed);
        }
        //Colision obstaculo
        if (collision.gameObject.name == "obstacle")
        {
            if (currentHealth > 0)
            {           
                currentHealth--;
                ActualizeHealth();
                //Podriamos añadir un pequeño efecto cuando colisiona con un obstaculo
                Destroy(collision.gameObject);
                PlayAudio(Audio_ObstacleHit);
            }
            else if (currentHealth == 0)
            {

                particleSystem.Play();
                lose = true;
                ActualizeHealth();
                  
               
                    //defeatGame dg = new defeatGame();
                    //dg.Quit();
                    
              
                   
               
            }
        }
    }
    
    public void ActualizeScore()
    {
        c_UI.ActualizeScoreText(currentScore);
    }
    public void ActualizeHealth()
    {
        c_UI.ActualizeHealthText(currentHealth);
    }

    void PlayAudio(AudioClip clip)
    {
        a_AudioSource.volume = PlayerPrefs.GetFloat("effects", 0.0f);
        a_AudioSource.PlayOneShot(clip);
    }
}
