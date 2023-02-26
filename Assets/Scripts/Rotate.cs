using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var eulerAngles = transform.eulerAngles;
        //eulerAngles.z += speed * Time.deltaTime;
        //eulerAngles.y += speed * Time.deltaTime;
        //eulerAngles.x += speed * Time.deltaTime;
        //transform.eulerAngles = eulerAngles;

        transform.RotateAround(new Vector3(5, 5, 1), Vector3.up, speed * Time.deltaTime);
    }
}
