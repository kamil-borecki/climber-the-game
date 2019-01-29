using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float xPower = 200f;
    public float yPower = 200f;
    public State state;
    private ConstantForce2D force;
    private PlayerController playerController;
    private Rigidbody2D rigid;
    private Animator headAnimator;
    private GameObject head;
    void Start()
    {
       force = this.GetComponent<ConstantForce2D>();
        head = GameObject.Find("Head");
        headAnimator = head.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        playerController = GameObject.Find("Camera").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xForce = 0;
        float yForce = 0;
        if (playerController.left)
        {
            xForce += -xPower;
            yForce = yPower;
        }
        if (playerController.right)
        {
            xForce += xPower;
            yForce = yPower;
        }
        if (!playerController.leftHandRef.isGrabbed && !playerController.rightHandRef.isGrabbed) {
            xForce = yForce = 0;
        }
        if(playerController.leftHandRef.isRaised || playerController.rightHandRef.isRaised)
        {

            yForce = yPower;
            headAnimator.SetBool("looking", true);
            if(playerController.leftHandRef.isRaised)
            {
                head.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                head.GetComponent<SpriteRenderer>().flipX = false;

            }
        }
        else
        {
            headAnimator.SetBool("looking", false);
        }
        force.force = new Vector2(xForce, yForce);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "end")
        {
            SceneManager.LoadSceneAsync("title");
        }
        if(collision.tag == "endThing")
        {
            playerController.leftHandRef.isGrabbed = false;
            playerController.rightHandRef.isGrabbed = false;
        }

    }
}
