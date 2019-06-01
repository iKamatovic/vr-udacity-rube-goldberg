using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour {

    public UnityEvent onThrowableCollision;
    public string throwable;

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == throwable) {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            onThrowableCollision.Invoke();
        }
    }
}
