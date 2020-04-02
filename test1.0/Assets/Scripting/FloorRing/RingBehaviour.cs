using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehaviour : MonoBehaviour
{
    [System.Serializable]
    public struct RingProps
    {
        public float TimerDirection;
        public float Velocity;
    }

    int a_Direction;
    float a_Timer;
    float scaleX;
    float scaleY;
    float scaleZ;
    float velocityScale = .000005f;
    Transform a_Transform;

    [SerializeField]
    RingProps a_Props;

    // Start is called before the first frame update
    void Start()
    {
        a_Transform = GetComponent<Transform>();
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Scale();
    }

    void SetUp()
    {
        a_Timer = a_Props.TimerDirection;
        a_Direction = 1;
    }

    void Rotate()
    {

        if (a_Timer > 0)
        {
            a_Timer -= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + a_Direction * a_Props.Velocity);
            a_Timer -= Time.deltaTime;
        }
        else
        {
            a_Timer = a_Props.TimerDirection;
            a_Direction *= -1;
        }

    }
    void Scale()
    {
        Vector3 initialScale = a_Transform.localScale;

        scaleX += velocityScale;
        scaleY += velocityScale;
        scaleZ += velocityScale;

        initialScale.x += scaleX;
        initialScale.y += scaleY;
        initialScale.z += scaleZ;

        a_Transform.localScale = initialScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        a_Props.Velocity = 0;
        Destroy(this.gameObject);
    }
}
