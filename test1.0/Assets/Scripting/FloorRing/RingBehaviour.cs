﻿using System.Collections;
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
    float velocityScale = .00000025f;
    Transform a_Transform;

    GameObject floor;
    GameObject[] floors;

    public Vector3 temp = new Vector3(0, 0, 0);



    [SerializeField]
    RingProps a_Props;

    // Start is called before the first frame update
    void Start()
    {
        a_Transform = GetComponent<Transform>();
        SetUp();
        getAllFloor();
        createObstacles();
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
        if (Time.timeScale != 0)
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
    }
    void Scale()
    {
        if (Time.timeScale != 0)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            createObstacles();
            
            Vector3 scaleInit = temp;

            //Instantiate(floor);
            if (scaleInit.x > .25f)
            {
                scaleInit.x -= .25f;
                scaleInit.y -= .25f;
                scaleInit.z -= .25f;
                a_Transform.localScale = scaleInit;
            }

            //Se agrega angulo aleatorio
            var euler = transform.eulerAngles;
            euler.z = Random.Range(0.0f, 360.0f);
            transform.eulerAngles = euler;
            
        }

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

    void createObstacles()
    {
        EdgeCollider2D MyEdgeCollider2D;
        int numObstacles = Random.Range(1, 3);
        for(int i = 0; i<numObstacles; i++)
        {
            MyEdgeCollider2D = GetComponent<EdgeCollider2D>();
            GameObject newChild = new GameObject("obstacle");
            newChild.transform.parent = transform;

            Vector2[] colliderpoints;
            int numPoint = Random.Range(0, 42);
            colliderpoints = MyEdgeCollider2D.points;
            newChild.transform.localPosition = new Vector3(colliderpoints[numPoint].x, colliderpoints[numPoint].y, 0.0f);

            newChild.transform.eulerAngles = new Vector3(
            newChild.transform.eulerAngles.x,
            newChild.transform.eulerAngles.y,
            newChild.transform.eulerAngles.z + 30
            );

            Vector3 scaleChild = new Vector3(.75f,.75f,.75f);
            newChild.transform.localScale = scaleChild;

            MeshFilter meshfilter = newChild.AddComponent<MeshFilter>();
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            meshfilter.mesh = go.GetComponent<MeshFilter>().mesh;
            meshfilter.gameObject.AddComponent<MeshRenderer>();
            Destroy(go);

            newChild.AddComponent<BoxCollider2D>();
            newChild.AddComponent<MeshRenderer>();

            newChild.GetComponent<BoxCollider2D>().isTrigger = true;
            newChild.AddComponent<Obstacle>();
        }
    
    }
}

