using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float maxSpeed = 24;

    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.magnitude >= maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }

    public float GetSpeed()
    {
        return body.velocity.magnitude;
    }
}
