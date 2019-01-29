using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public Camera cam;
    private BoxCollider2D groundCollider;
    private float groundHeight;


    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHeight = groundCollider.size.y * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(groundHeight);
    }

    void OnBecameInvisible()
    {
        if(cam.transform.position.y > transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + groundHeight * 2f);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - groundHeight * 2f);

        }
    }
}
