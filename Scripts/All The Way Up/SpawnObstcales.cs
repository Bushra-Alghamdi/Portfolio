using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstcales : MonoBehaviour
{
    

    public GameObject obstacles;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.name.Equals("Player"))
    //    {
    //       // Destroy(other.gameObject);
    //       print("here");
    //    }
    //}

   /* private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log("Got You!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }*/

    void Spawn()
    {
      /*  float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        float P = Mathf.Pow(-1, Random.Range(1, 3));
        transform.position = transform.position + new Vector3(P, 1);*/

        Instantiate(obstacles, transform.position, transform.rotation);
       

        Destroy(GameObject.Find("Block(Clone)"), 2);

        
    }

}
