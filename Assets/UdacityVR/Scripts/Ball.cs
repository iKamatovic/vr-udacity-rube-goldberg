using UnityEngine;
using UnityEngine.Events;


public class Ball : MonoBehaviour {

    public UnityEvent onGroundCollision;
    public string ground;
    public Color disableColor = new Color(1f, 0f, 0f, 1f);

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Color originalColor;
    private Rigidbody rb;
    private GameObject rendering;
    private Animator anim;

    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rendering = gameObject.transform.Find("Mesh").gameObject;
        anim = rendering.GetComponent<Animator>();
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;
        originalColor = rendering.GetComponent<Renderer>().material.color;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == ground) onGroundCollision.Invoke();
    }

    public void Restore() {
        Enable();
        anim.SetTrigger("Destroy");
    }

    public void Enable() {
        rendering.GetComponent<Renderer>().material.color = originalColor;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void Disable() {
        rendering.GetComponent<Renderer>().material.color = disableColor;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public void OnAnimationEnd() {
        anim.ResetTrigger("Destroy");

        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
