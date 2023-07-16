using UnityEngine;

public class FollowSpaceShip : MonoBehaviour
{
    public Transform spaceShip;
    public Vector3 cameraOffset;

    void Update()
    {
        transform.position = spaceShip.position + cameraOffset;
    }
}
