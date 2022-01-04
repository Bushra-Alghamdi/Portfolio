using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{

    public HealthBar healthBar;
    private Collider2D collider;
    public float maxHealth = 100;
    public ParticleSystem splash;
    public MovebyTouch player;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {

    }
    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           Splash();
           //healthBar.SetHealth(maxHealth);
            //RechargeSlowly();
        }
    }

    void Splash()
    {
      //  print("helloooo");
        splash.Play();
    }

    void RechargeSlowly()
    {
        float health = player.currentHealth;
        for (float i = health; i<maxHealth; i++)
        {
            player.currentHealth+=3;
            healthBar.SetHealth(player.currentHealth);
        }
        
    }
  
}
