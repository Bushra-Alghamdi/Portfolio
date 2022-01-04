using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private Animator animator;
    private bool dayCamera = true;
    public UniversalPR pr;

    [SerializeField] private CinemachineVirtualCamera vcam1; //day
    [SerializeField] private CinemachineVirtualCamera vcam2; //night


    private void Awake()
    {
        
        pr = FindObjectOfType<UniversalPR>();
    }



    public void SwitchCamera()
    {
        if (dayCamera)
        {
            animator.Play("NightCamera");
        }
        else
        {
            animator.Play("DayCamera");
        }
        dayCamera = !dayCamera;
    }

    public void SwitchPriority()
    {
        if (dayCamera)
        {
            vcam1.Priority = 2;
            vcam2.Priority = 3;
        }
        else
        {
            vcam1.Priority = 3;
            vcam2.Priority = 2;
        }
        dayCamera = !dayCamera;

    }
    public void DayCam()
    {
       
            vcam1.Priority = 3;
            vcam2.Priority = 2;
    }
    public void NightCam()
    {

        vcam1.Priority = 2;
        vcam2.Priority = 3;
    }

}
