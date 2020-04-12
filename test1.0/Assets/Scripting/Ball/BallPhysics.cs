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
    }

    // Update is called once per frame
    void Update()
    {

        KeyMapping();
        Rotation();
        MobileMovement();
        temp = rg_script.temp;
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
            a_rb.AddForce(transform.up * a_BallProps.JumpForce, ForceMode2D.Impulse);
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
                ActualizeHealth();
                defeatGame dg = new defeatGame();
                //dg.Quit();
                c_UI.Lose(currentScore);
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
        a_AudioSource.PlayOneShot(clip);
    }
}
