using UnityEngine;
using UnityEngine.WSA;

public class SpaceShipMovement : MonoBehaviour
{
    public Rigidbody spaceShip;
    public float spaceShipDefaultVelocity_X = 0f;
    public float spaceShipDefaultVelocity_Y = 0f;
    public float spaceShipDefaultVelocity_Z = 0f;
    public bool isLandscape = true;
    private Vector3 tilt;

    private void Start()
    {
        spaceShip.useGravity = false;
        spaceShip.AddForce(spaceShipDefaultVelocity_X * Time.deltaTime, spaceShipDefaultVelocity_Y * Time.deltaTime, spaceShipDefaultVelocity_Z * Time.deltaTime);
    }

    private void Update()
    {
        tilt = Input.acceleration;
    }

    void FixedUpdate()
    {
        if (isLandscape)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }
        spaceShip.AddForce(tilt);
    }
}
