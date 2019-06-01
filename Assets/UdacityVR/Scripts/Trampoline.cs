using UnityEngine;

public class Trampoline : MonoBehaviour {

    public float force = 10f;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Throwable")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * force, ForceMode.Impulse);

        }
    }
}
