using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * 30;
    }
}
