using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnCollected : UnityEvent<Collectable>
{
}

public class Collectable : MonoBehaviour {

    public OnCollected onCollected;
    public string collector;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == collector) onCollected.Invoke(this);
    }

    public void Remove() {
        gameObject.SetActive(false);
    }

    public void Restore() {
        gameObject.SetActive(true);
    }
}
