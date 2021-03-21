using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rigidbodyRef;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbodyRef = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - Vector3.zero).magnitude;

        if (distance>100)
        {

        }
    }

    public void SetVelocity(Vector3 direction)
    {
        rigidbodyRef.velocity = direction * speed;
    }
}
