using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;
    public bool movementKeyPressed = true;
    public bool stopmoving = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //void FixedUpdate()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");
    //    Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
    //    transform.Translate(movement*Time.deltaTime*speed);




    //}



    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        transform.position += Vector3.left * speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        transform.position += Vector3.right * speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        transform.position += Vector3.up * speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        transform.position += Vector3.down * speed * Time.deltaTime;
    //    }
    //}

    private void Update()
    {



        if (stopmoving == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (movementKeyPressed == false)
                    movementKeyPressed = true;
                else
                    movementKeyPressed = false;
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))

            {
                rb.velocity = new Vector2(transform.localScale.x * speed, 0);


            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))

            {
                rb.velocity = new Vector2(-transform.localScale.x * speed, 0);

            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))

            {
                rb.velocity = new Vector2(0, -transform.localScale.y * speed);

            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))

            {
                rb.velocity = new Vector2(0, transform.localScale.y * speed);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Border")
        {
            Debug.Log("Hit Border,- STOP!");
     
            movementKeyPressed = false;
            rb.velocity = new Vector2(0, 0);

        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit on Border....");


       
    }




}