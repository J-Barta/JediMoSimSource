using UnityEngine;

public class Magic4414CubeEjector : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Cube"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.back * 20);
        }
    }
}
