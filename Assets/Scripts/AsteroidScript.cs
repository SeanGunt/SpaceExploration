using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{   
    Rigidbody2D rigidbody2d;
    public static float speed = 3.5f;
    public Transform target;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody2d.position = Vector2.MoveTowards(rigidbody2d.position, target.position, speed * Time.deltaTime);

        transform.Rotate (0,0,20 * Time.deltaTime);
        
    }
}
