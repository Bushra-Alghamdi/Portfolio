using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public AudioClip ObstacleSFX;
    //Rigidbody2D rigid;
    public GameObject barrier;
    public float moveX = 5;
    public float moveY = 5;
    
    void start()
    {
        //rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {/*
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            StartCoroutine(OneStepRight());
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(OneStepRight());

        }*/
        //StartCoroutine(EnemyWalk());
    }

    /* void Fixedupdate()
     {
         // rigid.velocity = new Vector2(3,0);
         rigid.AddForce(Vector2.right * 5);

         //transform.position += new Vector3(1, -1, 0);
     }
 */
    private void OnTriggerEnter2D(Collider2D other)
    {
   
        //if (other.gameObject.tag == "Obstacle Bolck")
        //{
        //    Destroy(gameObject);
           
        //    //GetComponent<AudioSource>().PlayOneShot(ObstacleSFX);
        //}
        

    }
   /* private void OnCollision2D(Collider2D other)
    {
    
        if(other.gameObject.tag == "Block")
        {
            StartCoroutine(EnemyWalk());
        }
    }
*/
    public IEnumerator EnemyWalk()
    {
        transform.position += new Vector3(1, 0f, 0);
        //yield return new WaitForSeconds(1);
        yield return new WaitForSecondsRealtime(1);
        transform.position += new Vector3(0, -1f, 0);

    }

    private static readonly List<Vector3> RightArc = new List<Vector3>()
    {
        new Vector3(0.0f, 1f),
        new Vector3(0.4f, 0.9f),
        new Vector3(0.9f, 0.4f),
        new Vector3(1, 0)

    /*    new Vector3(0.0f, 1.0f),
        new Vector3(0.5f, -0.9f),
        new Vector3(0.9f, -0.5f),
        new Vector3(1.0f, 0f)*/
    };

    private static readonly List<Vector3> RightArcDirections = new List<Vector3>()
    {
        RightArc[1] - RightArc[0],
        RightArc[2] - RightArc[1],
        RightArc[3] - RightArc[2]
    };

    public Vector3 blockSize = new Vector3(3f, 3f, 0f);
    private int counter = 0;
    private Vector3 originalPosition;
    public IEnumerator OneStepRight()
    {
        counter = 0;
        originalPosition = transform.position;
        
        for (int s = 0; s < 3; s++)
        {
            transform.position += new Vector3(RightArcDirections[s].x * blockSize.x,  RightArcDirections[s].y * blockSize.y, 0f);
                yield return new WaitForSeconds(0.5F*Time.deltaTime);
        }


        

    }

    public IEnumerator Stp()
    {
        yield return new WaitForSeconds(0.5F);
        StartCoroutine(OneStepRight());
    }

/*    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death" )
        {
            print("Die and go to hell");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4, 0);
            gameObject.GetComponent<Rigidbody2D>().bodyType =RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            
        }
    }*/

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Obstacle Bolck")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(moveX, moveY,0);
        }
    }

}
//new Vector3(0.05f, 0.3f),
