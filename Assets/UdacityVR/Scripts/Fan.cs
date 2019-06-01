using UnityEngine;

public class Fan : MonoBehaviour {

    public float strength = 80f;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Throwable") {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * strength);
        }
    }
}
