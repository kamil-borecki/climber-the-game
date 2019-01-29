using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isLeft = false;
    public bool isGrab = false;
    public float yPower = 200f;
    private Rigidbody2D rigid;
    private PlayerController playerController;
    private ConstantForce2D force;
    private bool isOverShelf = false;
    public bool isGrabbed = false;
    public bool isRaised = false;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        force = this.GetComponent<ConstantForce2D>();

        playerController = GameObject.Find("Camera").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
     
        float yForce = 0;
        if (isRaised)
        {
            isGrabbed = false;
            rigid.constraints = RigidbodyConstraints2D.None;
            yForce = yPower;
        }

        if (!isGrabbed)
        {
            rigid.constraints = RigidbodyConstraints2D.None;

        }
        if (!playerController.leftHandRef.isGrabbed && !playerController.rightHandRef.isGrabbed)
        {
            yForce = 0;
        }
        force.force = new Vector2(0, yForce);

        if (isGrab)
        {
            gameObject.GetComponent<Animator>().SetBool("grabbed", isGrabbed);

        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "shelf")
        {
            isOverShelf = true;
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "shelf")
        {
            isOverShelf = false;
        }

    }
    void HandEnd()
    {

      
        if (isGrab && isOverShelf && !isGrabbed)
        {
            isGrabbed = true;
            FindObjectOfType<Player>().GetComponent<AudioSource>().Play();
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
           
               playerController.SetGrabbed(isLeft);


        }
   
    }

}
