using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
     Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
           // print("ouch");
            StartCoroutine(BlockReaction());
            //transform.position = new Vector2 (transform.position.x ,0.0001f*Time.deltaTime);
        }
        //if (collision.gameObject.tag.Equals("Obstacle"))
        //{
        //   Destroy(collision.gameObject);
        //}
    }

    IEnumerator BlockReaction()
    {
        //yield return new WaitForSeconds(0.1f);
        rb.AddForce(transform.up * -25f);
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(transform.up * 40f);
        yield return new WaitForSeconds(0.4f);
        rb.AddForce(transform.up * -13f);
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(transform.up * -2f);


    }
}
