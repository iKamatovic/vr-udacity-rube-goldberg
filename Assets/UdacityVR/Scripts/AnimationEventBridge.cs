using UnityEngine;
using UnityEngine.Events;

public class AnimationEventBridge : MonoBehaviour {

    public UnityEvent onAnimationEnd;

    void OnAnimattionEnd() {
        onAnimationEnd.Invoke();
    }
}
