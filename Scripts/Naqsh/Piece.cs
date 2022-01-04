using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{


   
    public bool placed = false;

    
    public GameObject pickedObject;
    
    public float rotationAngle;

 

    public void Start()
    {
    }
    private void Update()
    {
     
        
        gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);


    }

    void OnMouseDown()
    {

        
    }

    public void GetMousePos(Piece piece)
    {

       

        if (piece.pickedObject == gameObject)
        {
            Vector2 RotatePoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
           
            float newRot = Mathf.Atan2(RotatePoint.y, RotatePoint.x) * Mathf.Rad2Deg - 90;
          

        }
    }

    
}
