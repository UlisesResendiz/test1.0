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

    GameObject floor;
    GameObject[] floors;

    Vector3 temp = new Vector3(0, 0, 0);

    [SerializeField]
    RingProps a_Props;

    // Start is called before the first frame update
    void Start()
    {
        a_Transform = GetComponent<Transform>();
        SetUp();
        getAllFloor();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Scale();
        updatefloor();
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
       
        Vector3 scaleInit = temp;
        //Instantiate(floor);
        scaleInit.x -= .25f;
        scaleInit.y -= .25f;
        scaleInit.z -= .25f;
        a_Transform.localScale = scaleInit;
   
        //floors = GameObject.FindGameObjectsWithTag("Floor");
        //for (int i = 0; i < floors.Length; i++)
        //{
          //  Destroy(floors[i].gameObject);

        //}
        //Destroy(this.gameObject);
    }

    void getAllFloor()
    {
        floors = GameObject.FindGameObjectsWithTag("Floor");
            
    }

    void updatefloor()
    {
              if (floors[2].gameObject.transform.localScale.x < floors[0].gameObject.transform.localScale.x && floors[2].gameObject.transform.localScale.x < floors[1].gameObject.transform.localScale.x)
                {
                temp = floors[2].gameObject.transform.localScale;
                return;
                 }

                else if (floors[0].gameObject.transform.localScale.x < floors[1].gameObject.transform.localScale.x && floors[0].gameObject.transform.localScale.x < floors[2].gameObject.transform.localScale.x)
                {
                temp = floors[0].gameObject.transform.localScale;
                return;
                }

                 else if (floors[1].gameObject.transform.localScale.x < floors[2].gameObject.transform.localScale.x && floors[1].gameObject.transform.localScale.x < floors[0].gameObject.transform.localScale.x)
                {
                temp = floors[1].gameObject.transform.localScale;
                return;
                } 

    }
}

