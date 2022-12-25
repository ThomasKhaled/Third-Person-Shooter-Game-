using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // right (1,0,0) , left(-1,0,0) , forward(0,0,1) , back(0,0,-1)
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
