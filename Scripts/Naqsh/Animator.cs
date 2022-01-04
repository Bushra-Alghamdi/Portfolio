using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    public IEnumerator Animate(bool fadeIn)
    {
       
        print("animate");
        
           
                sprite.color = new Color(1, 1, 1, 1);
                yield return null;
            
        

    }
}
