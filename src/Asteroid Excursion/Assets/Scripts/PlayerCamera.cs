using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform lookAt;

    private Vector3 desiredPosition;
    private float offset = 300f;
    private float disatance = 900f;

    private void Update()
    {
        desiredPosition = lookAt.position + (-transform.forward * disatance) + (transform.up * offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);

        transform.LookAt(lookAt.position + (Vector3.up * offset));
    }

}
