using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Rotation : MonoBehaviour
{
    float rotationSpeed = 90f;
    float maxRotation = 360;
    public bool placed;
    private bool isStarted;
    public float smallAnglePlace;
    public float bigAnglePlace;
    public GameObject animator;

    public Piece piece;

    [SerializeField] public AudioSource _source;
    [SerializeField] public AudioClip _mouseDown, _mouseUp, _win;
    // Start is called before the first frame update
    void Start()
    {

        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {


        bool turtle = false;
        RaycastHit hit;
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

        var playerPlane = new Plane(forward, transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitdist = 0.0f;

        if (!placed && Input.GetMouseButton(0))
        {
           // _source.PlayOneShot(_mouseDown);

            // Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (playerPlane.Raycast(ray, out hitdist))
            {
                var target = ray.GetPoint(hitdist);

                var relative = transform.InverseTransformPoint(target);
                var angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

                var maxRotation = rotationSpeed * Time.deltaTime;
                var clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);

                if (Mathf.Abs(angle) > 10) //takes out some jitters, but loses a small amount of accuracy.
                    transform.Rotate(0, 0, clampedAngle);


              //  transform.Rotate(Vector3.Lerp(transform.rotation.eulerAngles, transform.rotation.eulerAngles * 3, 2f));

            }

            
           
        }


        if (!Input.GetMouseButton(0))
        {
            if (gameObject.transform.rotation.z <= bigAnglePlace && gameObject.transform.rotation.z >= smallAnglePlace && !placed)
            {

                placed = true;
                animator.SetActive(true);
                print("placed");

                _source.PlayOneShot(_win);
                StartCoroutine(MoveToScene(3f));
             //   Invoke("Win", 3f);

            }
        }

        if (!placed && Input.GetMouseButtonUp(0))
        {
            _source.PlayOneShot(_mouseUp);
        }

        if (!placed && Input.GetMouseButtonDown(0))
        {
            _source.PlayOneShot(_mouseDown);
        }


    }

    /*void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    IEnumerator MoveToScene(float time)
    {
        //execute before wait

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //execute afte wait

    }

}
