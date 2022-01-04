using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentPieces : MonoBehaviour
{



    //  private Vector2 piecePoint;
    public float rotationAngle;
  

    public void Start()
    {

    }
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);



    }

    

    public void GetMousePos(DifferentPieces piece)
    {

        
            Vector2 RotatePoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        
            float newRot = Mathf.Atan2(RotatePoint.y, RotatePoint.x) * Mathf.Rad2Deg - 90;
           

        
    }

   


}

