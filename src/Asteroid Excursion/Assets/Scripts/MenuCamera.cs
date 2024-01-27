using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    public Transform shopWaypoint;
    public Transform levelWaypoint;

    private void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;

        desiredPosition = startPosition;
        desiredRotation = startRotation;
    }

    private void Update()
    {
        Vector3 inputs = Manager.Instance.GetPlayerInput();

        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition + new Vector3(inputs.x, 0, 0) * 10f, Time.deltaTime * 2f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, desiredRotation, Time.deltaTime * 2f);
    }

    public void BackToMainMenu()
    {
        desiredPosition = startPosition;
        desiredRotation = startRotation;
    }

    public void MoveToShop()
    {
        desiredPosition = shopWaypoint.localPosition;
        desiredRotation = shopWaypoint.localRotation;
    }

    public void MoveToLevel()
    {
        desiredPosition = levelWaypoint.localPosition;
        desiredRotation = levelWaypoint.localRotation;
    }


}
