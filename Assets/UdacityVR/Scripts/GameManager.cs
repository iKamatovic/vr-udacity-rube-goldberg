using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour {

    public SteamVR_Action_Boolean MenuAction;
    public MenuManager menu;
    public Ball ball;
    public VRProxy vrProxy;
    public GameObject gameOver;
    public string nextLevel;

    private GameObject[] collectables;
    private int numberOfCollectedItems = 0;
    private bool ballPickedUp = false;

    void Start() {
        collectables = GameObject.FindGameObjectsWithTag("Collectible");
    }

    void Update() {
        if (MenuAction.GetState(SteamVR_Input_Sources.RightHand)) {
            if (!menu.isActiveAndEnabled) menu.Show();
        }
        else {
            if (menu.isActiveAndEnabled) menu.Hide();
        }

        if (ballPickedUp) {
            if (!IsValidPosition()) ball.Disable();
            else ball.Enable();
        }
    }

    void OnDestroy() {
        DestroyBall();
        DestroyCollectables();
        DestroyStructures();
    }

    public void OnCollected(Collectable item) {
        numberOfCollectedItems++;
        item.Remove();
    }

    public void OnGoalReached() {
        if (IsCompleted()) {
            if (nextLevel != "") LoadNextLevel();
            else EndGame();
        }
        else Restore();
    }

    public void OnBallPickedUp() {
        ballPickedUp = true;
    }

    public void OnBallDetachedFromHand() {
        ballPickedUp = false;

        if (!IsValidPosition()) {
            ball.Restore();
        }
    }

    public void OnGroundCollision() {
        Restore();
    }

    public void OnMenuItemSelect(MenuItem menuItem, Transform transform) {
        Instantiate(menuItem.obj, transform.position, transform.rotation);
        menu.UpdateAmount();
        menu.Hide();
    }

    private bool IsCompleted() {
        return collectables.Length == numberOfCollectedItems;
    }

    private bool IsValidPosition() {
        RaycastHit ray;
  
        if (Physics.Raycast(vrProxy.GetCameraPosition(), -Vector3.up, out ray)) {
            return ray.collider.CompareTag("Platform");
        }

        return false;
    }

    private void LoadNextLevel() {
        SteamVR_LoadLevel.Begin(nextLevel);
    }

    private void EndGame() {
        if (gameOver) gameOver.SetActive(true);
        ball.gameObject.SetActive(false);
    }

    private void Restore() {
        numberOfCollectedItems = 0;
        RestoreCollectables();
        ball.Restore();
    }

    private void DestroyBall() {
        Destroy(ball.gameObject);
    }

    private void DestroyStructures() {
        GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");

        foreach (GameObject structure in structures) {
            Destroy(structure);
        }
    }

    private void DestroyCollectables() {
        foreach (GameObject collectable in collectables) {
            Destroy(collectable);
        }
    }

    private void RestoreCollectables() {
        foreach (GameObject collectable in collectables) {
            collectable.GetComponent<Collectable>().Restore();
        }
    }
}
