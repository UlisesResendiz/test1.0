using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    public static BallPhysics a_BallPhysics;

    Rigidbody2D a_rb;

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


    private void Awake()
    {
        a_BallPhysics = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        a_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyMapping();
        Rotation();
        MobileMovement();
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
        transform.position = transform.position + Direction * a_BallProps.RotationVel * Time.deltaTime;
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

    
}
