using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
//using EZCameraShake;
using Spine.Unity;
using UnityEngine.UI;


public class BushraApproach : MonoBehaviour
{
    
    public SoundManager soundManager;
/*    public Obstacle obstacle;
*/
    public UniversalPR pr;
    public CameraController cameraController;

    public SkeletonAnimation skeletonAnimation;
    //public AnimationReferenceAsset health;


    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset jump, death;
    private Rigidbody2D rb;
    public HealthBar healthBar;
    private Collider2D myCollider;
    public GameObject deathCollider;
    public GameObject PlayerRespawn;
    public GameObject LosePanel;
    public GameObject WinPanel;
    public Transform playerTransform;
    bool isOutside = false;
    bool isMoving = false;
    bool isPlaying = false;
    bool isChallenge = false;
    bool onRecharge = false;
    bool hasWon = false;

    public float stepSpeed = 5f;
    private float move = 1.0f;
    public float moveLR;
    private float x;
    private float y;

    public List<AudioClip> audioClips = new List<AudioClip>();

    //int soundIterature= 0 ;



    public int flySpeed = 2000;

    public float maxHealth = 100;
    public float currentHealth;
    public float StaminaBarSpeed = 2f;
    public float Recharging;

    
    public ParticleSystem dust;
    public ParticleSystem coinCollecting;
    public ParticleSystem Flying;
    public ParticleSystem respawnPR;
    public ParticleSystem rechargePr;
    public ParticleSystem Angry;
    public ParticleSystem sad;
    // public ParticleSystem respawn;

    public GameObject backgroundLayer1;
    public GameObject backgroundLayer2;
    public GameObject backgroundLayer3;
    public GameObject dizzy;


    bool lose = false;

    bool win = false;
    //bool respawned = false;

    public Transform[] playerSpawnPoints;

    private Animator anim;

    private string currentAnimation;
    private string currentState;
    public float jumpSpeed = 5.0f;

    private string previousState;

    float increaseTime = 0;
    public float increaseTimeInterval = 2;

    //public HealthBar health;
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        // prevents the same animation from restarting over and over 
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }

        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timescale;


        //update Line
        animationEntry.Complete += AnimatioEntry_Complete;

        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {

        if (state.Equals("jump"))
        {
            //note: jumping animation timescale is set to 2 
            SetAnimation(jump, true, 2);
        }
        else if (state.Equals("death"))
        {
            SetAnimation(death, false, 1);
        }

        if (state.Equals("idle"))
        {
            SetAnimation(idle, true, 1);
        }

        previousState = currentState;
        currentState = state;
    }

    public void Death()
    {
        if (!currentState.Equals("death"))
        {
            //previousState = currentState;
            SetCharacterState("death");
        }
    }
    //use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        Recharging = currentHealth;
        healthBar.SetMaxHealth(maxHealth);

        anim = GetComponent<Animator>();
       
        currentState = "idle";

    }



    //update is called once per farme
    void Update()
    {
        KeyboardMove();
        TouchMove();

        if (onRecharge.Equals(true))
        {
            IncreaseHealthBar();
        }
        else if(hasWon.Equals(false))
        {
            DecreaseHealthBar(StaminaBarSpeed);
        }
        if (currentHealth <= 0 || lose)
        {
            LosePanel.SetActive(true);
            Time.timeScale = 0;
            if (isPlaying.Equals(false))
            {
                FindObjectOfType<SoundManager>().PlaySFX("Lose");
                isPlaying = true;

            }

        }

        //if(isOutside)
        //   StartCoroutine(RespawnPlayer());

    }

    //called for touch screen

    public void TouchMove()
    {
        

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height * 0.75)
        {


            if (Time.timeScale != 0 && isOutside == false)
            {
                
                x = Input.mousePosition.x / (float)Screen.width;

                if (x > 0.5)
                {

                    if (!currentState.Equals("idle"))
                    {
                        SetCharacterState("jump");
                    }
                    RightStep();

                   // DecreaseHealthBar(3);

                    //rb.AddForce(new Vector2(1, 1));
                    //Vector2 playerVelocity = new Vector2(rb.velocity.x * Speed * Time.deltaTime, rb.velocity.y);

                }

                if (x < 0.5)
                {

                    if (!currentState.Equals("idle"))
                    {
                        SetCharacterState("jump");
                    }
                    LeftStep();
                   // DecreaseHealthBar(3);

                    //rb.AddForce(new Vector2(-1, 1));
                    //Vector2 playerVelocity = new Vector2(-rb.velocity.x * Speed * Time.deltaTime, rb.velocity.y);
                }

            }
            else
            {
                
                SetCharacterState("idle");
                
            }
        }

    }

    void KeyboardMove()
    {
      

        if (Time.timeScale != 0 && isOutside == false)
        {
           
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SetCharacterState("jump");
                RightStep();
                //StartCoroutine(obstacle.OneStepRight());
                // DecreaseHealthBar(3);
                //increaseTime = 0;
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SetCharacterState("jump");
                LeftStep();
                //StartCoroutine(obstacle.OneStepRight());
               // DecreaseHealthBar(3);                                      // 3 = this is the amount of points you lose every step, you change it bigger for faster point losing
               // increaseTime = 0;                                          //  also in lines 221, 236, 264
            }
        }
        else
        {
            
            SetCharacterState("idle");
           
        }

        //increaseTime += Time.deltaTime+ 0.2f;
        //if(increaseTime >= increaseTimeInterval)
        //{
        //    StartCoroutine(IncreaseHealthBar());
        //    increaseTime = 0;
        //}
    }


    void RightStep()
    {
       

        if (isChallenge.Equals(true))
        {
            move = -1;
            transform.localScale = new Vector2(-0.76f, 0.76f);
            StartCoroutine(OneStepLeft());
        }
        else
        {
            move = 1;
            transform.localScale = new Vector2(0.76f, 0.76f);
            StartCoroutine(OneStepRight());
        }


        //transform.position += new Vector3(move, 1.0f, 0);
        //moveLR = Mathf.Lerp(1, Input.GetAxis("Horizontal") * 50, 3*Time.deltaTime);

        isMoving = true;
        StepsAudio();
        Dust();

        //anim.SetBool("isWalking", true);

        //anim.SetBool("isIdle", true);
    }

    void LeftStep()
    {
        

        if (isChallenge.Equals(true))
        {
            move = 1;
            transform.localScale = new Vector2(0.76f, 0.76f);
            StartCoroutine(OneStepRight());
        }
        else
        {
            move = -1;
            transform.localScale = new Vector2(-0.76f, 0.76f);
            StartCoroutine(OneStepLeft());
        }
        //transform.position += new Vector3(move, 1.0f, 0);


        isMoving = true;
        StepsAudio();
        Dust();

        //anim.SetBool("isWalking", true);
        //anim.SetBool("isIdle", true);
    }
    /*
        private void DecreaseHealthBar(float amount)
        {



                currentHealth -= amount ;                     
                healthBar.SetHealth(currentHealth);
            print(currentHealth);



        }

        private IEnumerator IncreaseHealthBar()
        {
            yield return new WaitForSeconds(3f);
            for (float i = currentHealth; i <= maxHealth; i++)
            {
                currentHealth += 2 * Time.deltaTime;                    // here is the increasement of health, for faster increasement you can use a bigger number. and  vice versa
                print(currentHealth);
                healthBar.SetHealth(currentHealth);
            }
        }
    */

    public void DecreaseHealthBar(float hhg)
    {
        
        if (onRecharge.Equals(false))
        {
            currentHealth -= hhg * Time.deltaTime;
            healthBar.SetHealth(currentHealth);
        }


    }

    public void IncreaseHealthBar()
    {
        for (float i = currentHealth; i <= maxHealth; i++)
        {
            currentHealth += 10f * Time.deltaTime;
            healthBar.SetHealth(currentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "ObstacleAI")
        {
           // FindObjectOfType<SoundManager>().PlaySFX("ObstacleSFX");
            print("HERE");


            //currentHealth -= 3;
            //healthBar.SetHealth(currentHealth);
            StartCoroutine(RespawnPlayer());
        }

        if (col.gameObject.tag == "Recharge")
        {
            onRecharge = true;
            IncreaseHealthBar();
            FindObjectOfType<SoundManager>().PlaySFX("RechargeSFX");
            rechargePR();
           // Destroy(col.gameObject);


        }

        if (col.gameObject.tag == "Lavender")
        {
            StartCoroutine(JumpToWin());
            Destroy(col.gameObject);
            lose = false;
            hasWon = true;
            FlyingPR();
          
        }

        //here code the destroy coins 
        if (col.gameObject.CompareTag("Coins"))
        {
            FindObjectOfType<SoundManager>().PlaySFX("CollectSFX");

            Destroy(col.gameObject);
            Coin();
           
        }

        if (col.gameObject.CompareTag("Winning"))
        {
            win = true;
            Time.timeScale = 0;
            WinPanel.SetActive(true);
            FindObjectOfType<SoundManager>().PlaySFX("WinSFX");

        }
        if (col.gameObject.CompareTag("Death"))
        {

            isOutside = false;
        }

        if (col.gameObject.CompareTag("Challenge"))
        {
            
            //pr.day = true;
            //pr.DayOrNight();
            cameraController.NightCam();
            dizzy.SetActive(true);
            backgroundLayer1.SetActive(false);
            backgroundLayer2.SetActive(false);
            backgroundLayer3.SetActive(false);
            isChallenge = true;
            
        }

        if (col.gameObject.CompareTag("EndChallenge"))
        {
            //pr.day = true;
            //pr.DayOrNight();
            cameraController.DayCam();
            backgroundLayer1.SetActive(true);
            backgroundLayer2.SetActive(true);
            backgroundLayer3.SetActive(true);
            isChallenge = false;
            dizzy.SetActive(false);
        }
        //if (col.gameObject.CompareTag("Challenge"))
        //{
        //   pr.day = true;
        //   pr.DayOrNight();
        //}

        if (col.gameObject.CompareTag("Death2"))
        {
            isOutside = true;
            // print("" + isOutside);
            DecreaseHealthBar(10f);
            //currentHealth -= 5;
            //healthBar.SetHealth(currentHealth);
            StartCoroutine(RespawnPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Death"))
        {
            isOutside = true;
            //print("" + isOutside);
            DecreaseHealthBar(10f);
            StartCoroutine(RespawnPlayer());


        }

        if (other.gameObject.tag == "Recharge")
        {
            onRecharge = false;

        }

    }

    IEnumerator JumpToWin()
    {

        //deathCollider.SetActive(false);
        rb.AddForce(new Vector2(0, -20));
        yield return new WaitForSecondsRealtime(0.5f);
        rb.AddForce(new Vector2(0, flySpeed));
        FindObjectOfType<SoundManager>().PlaySFX("winning");
    }



    private static readonly List<Vector3> RightArc = new List<Vector3>()
    {
        new Vector3(0.0f, 0f),
        //new Vector3(0.05f, 0.3f),
        new Vector3(0.2f, 0.6f),
        //new Vector3(0.5f, 0.86f),
        new Vector3(0.7f, 0.95f),
        new Vector3(1, 1)
    };


    private static readonly List<Vector3> RightArcDirections = new List<Vector3>()
    {
        RightArc[1] - RightArc[0],
        RightArc[2] - RightArc[1],
        RightArc[3] - RightArc[2],
        //RightArc[4] - RightArc[3],
        //RightArc[5] - RightArc[4]
    };

    private static readonly List<Vector3> LeftArc = new List<Vector3>()
    {
        new Vector3(-0.0f, 0f),
        //new Vector3(-0.05f, 0.3f),
        new Vector3(-0.2f, 0.6f),
        //new Vector3(-0.5f, 0.86f),
        new Vector3(-0.7f, 0.95f),
        new Vector3(-1, 1)
    };

    private static readonly List<Vector3> LeftArcDirections = new List<Vector3>()
    {
        LeftArc[1] - LeftArc[0],
        LeftArc[2] - LeftArc[1],
        LeftArc[3] - LeftArc[2],
        //LeftArc[4] - LeftArc[3],
        //LeftArc[5] - LeftArc[4]
    };

    private int counter = 0;
    private Vector3 originalPosition;

    //todo this needs to be set based on the actual block size
    public Vector3 blockSize = new Vector3(3f, 3f, 0f);

    IEnumerator OneStepRight()
    {
        isMoving = true;
        counter = 0;
        originalPosition = transform.position;

        for (int s = 0; s < 3; s++)
        {
            if (isMoving)
            {
                transform.position += new Vector3(RightArcDirections[s].x * blockSize.x,
                    RightArcDirections[s].y * blockSize.y, 0f);

                yield return new WaitForSeconds(stepSpeed * Time.deltaTime);
            }
        }
      
        yield break;
    }

    IEnumerator OneStepLeft()
    {
        isMoving = true;
        counter = 0;
        originalPosition = transform.position;

        for (int s = 0; s < 3; s++)
        {
            if (isMoving)
            {
                transform.position += new Vector3(LeftArcDirections[s].x * blockSize.x,
                LeftArcDirections[s].y * blockSize.y, 0f);

                yield return new WaitForSeconds(stepSpeed * Time.deltaTime);
            }
        }
       
    }

   
    public Vector2 RespawnAtNearestPoint(Vector2 curPosition)
    {

        Transform closestPoint = playerSpawnPoints[0];
        float shortestDistance = Vector2.Distance(curPosition, closestPoint.position);
        foreach (Transform trans in playerSpawnPoints)
        {
            float curDistance = Vector2.Distance(curPosition, trans.position);
            if (trans.position.y <= curPosition.y && shortestDistance > curDistance)
            {
                shortestDistance = curDistance;
                closestPoint = trans;
            }
        }

        return closestPoint.position;

    }

    IEnumerator RespawnPlayer()
    {
        SadPR();
        isOutside = true;
        yield return new WaitForSeconds(2f);
        RespawnPR();
        AngryPr();
        transform.position = RespawnAtNearestPoint(transform.position);
        FindObjectOfType<SoundManager>().PlaySFX("RespawnSFX");
        isOutside = false;
       

    }

    public void StepsAudio()
    {
       
        int i = UnityEngine.Random.Range(0, audioClips.Count);
        var clip = audioClips[i];
        GetComponent<AudioSource>().PlayOneShot(clip);


    }

    void Dust()
    {
        dust.Play();
    }

    void Coin()
    {
        coinCollecting.Play();
    }

    void FlyingPR()
    {
        Flying.Play();
    }

    void RespawnPR()
    {
        respawnPR.Play();
    }
    void rechargePR()
    {
        rechargePr.Play();
    }

    void AngryPr()
    {
        Angry.Play();
    }
    void SadPR()
    {
        sad.Play();
    }
    private void AnimatioEntry_Complete(Spine.TrackEntry trackEntry)
    {
        Debug.Log(previousState);
        currentState = previousState;
        SetCharacterState(currentState);

    }

    public bool IsGameOver()
    {
        return win || lose;
    }

    public void GameLost()
    {
        lose = true;
    }
}


