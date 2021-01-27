using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;
   
    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //give a constant speed to the ball
        //i guess lol
        body.velocity = body.velocity.normalized * speed*Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "extraBall")
        {
            Debug.Log("Ah sexy");
            FindObjectOfType<ballLauncher>().newAddedBallCount++;

        }
    }
}
