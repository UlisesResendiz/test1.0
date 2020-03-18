using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour
{

    [SerializeField]
    float a_VelRotation;

    public static BallRotation a_BallRotation;

    int a_RotDir;

    private void Awake()
    {
        a_BallRotation = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyMapping();

        AutomaticRotation();
    }

    void SetUp()
    {
        a_RotDir = 0;
    }

    void AutomaticRotation()    //For Mobile Use
    {
        if (a_RotDir == 1)
        {
            Rotate(1);
        }
        else if (a_RotDir == -1)
        {
            Rotate(-1);
        }
    }

    public void SetDirRotation(int value)
    {
        a_RotDir = value;
    }

    void KeyMapping()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Rotate(-1);
        }else if (Input.GetKey(KeyCode.A))
        {
            Rotate(1);
        }
    }

    public void Rotate(int Direction)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                transform.rotation.eulerAngles.y,
                                                transform.rotation.eulerAngles.z + Direction * a_VelRotation * Time.deltaTime);
    }
}
