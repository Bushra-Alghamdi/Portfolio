using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackground : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    [SerializeField] private Vector2 parallexEffectMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3( deltaMovement.x *parallexEffectMultiplier.x,deltaMovement.y*parallexEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;
    }
}
