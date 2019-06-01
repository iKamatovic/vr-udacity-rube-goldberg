using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR;

[System.Serializable]
public class MenuItem {
    public GameObject obj;
    public string label;
    public int amount;
}

[System.Serializable]
public class OnMenuItemSelected : UnityEvent<MenuItem, Transform>
{
}

public class MenuManager : MonoBehaviour {

    public SteamVR_Action_Vector2 swipe;
    public SteamVR_Action_Boolean spawn;
    public List<MenuItem> menuItems;
    public OnMenuItemSelected OnSelect;
    public float spawnWaitTime = 1f;
    private int current = 0;
    private float timeSinceLastSpawn = 0f;
    private bool lockSwipe = false;
    private List<GameObject> children = new List<GameObject>();
    private List<Text> labels = new List<Text>();

    void Start() {
        menuItems.ForEach(menuItem => {
            GameObject item = new GameObject();
            GameObject asset = Instantiate(menuItem.obj, Vector3.zero, Quaternion.identity);
            GameObject label = CreateLabel(menuItem);

            item.transform.SetParent(gameObject.transform);
            asset.transform.SetParent(item.transform);
            label.transform.SetParent(item.transform);

            item.transform.position = gameObject.transform.position + new Vector3(0, 0, 2f);

            DisableColiders(item);

            item.SetActive(false);
            children.Add(item);
            labels.Add(label.transform.Find("Text").GetComponent<Text>());
        });

        children[current].SetActive(true);

        timeSinceLastSpawn = spawnWaitTime;
    }

    void Update() {
        if (spawn.GetState(SteamVR_Input_Sources.RightHand) && timeSinceLastSpawn > spawnWaitTime) Spawn();
        timeSinceLastSpawn += Time.deltaTime;

        float swipeX = swipe.GetAxis(SteamVR_Input_Sources.RightHand).x;

        if (swipeX > 0.5f && !lockSwipe)
        {
            Next();
            lockSwipe = true;
        }

        if (swipeX < -0.5f && !lockSwipe)
        {
            Previous();
            lockSwipe = true;
        }

        if (swipeX > -0.5f && swipeX < 0.5f) {
            lockSwipe = false;
        }
    }

    void Next() {
        children[current].SetActive(false);
        current = Mathf.Min(current + 1, menuItems.Count - 1);
        children[current].SetActive(true);

    }

    void Previous() {
        children[current].SetActive(false);
        current = Mathf.Max(current - 1, 0);
        children[current].SetActive(true);
    }

    void Spawn() {
        if (menuItems[current].amount > 0) {
            OnSelect.Invoke(menuItems[current], children[current].transform);
            timeSinceLastSpawn = 0f;
        }
    }

    public void UpdatePosition(SteamVR_Behaviour_Pose pose, SteamVR_Input_Sources s) {
        gameObject.transform.position = pose.transform.position;
        gameObject.transform.rotation = pose.transform.rotation;
    }

    public void UpdateAmount() {
        menuItems[current].amount = Mathf.Max(0, menuItems[current].amount - 1);
        labels[current].text = menuItems[current].label + " (Available: " + menuItems[current].amount + ")";
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void DisableColiders(GameObject item) {

        foreach (Collider colider in item.GetComponents<Collider>()) {
            colider.enabled = false;
        }

        foreach (Collider colider in item.GetComponentsInChildren<Collider>()) {
            colider.enabled = false;
        }

    }

    private GameObject CreateLabel(MenuItem item) {
        Font fontArial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        GameObject label = new GameObject();
        label.name = "Label";
        label.AddComponent<Canvas>();
        label.AddComponent<CanvasScaler>();

        Canvas canvas = label.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler scaler = label.GetComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 100;
        scaler.referencePixelsPerUnit = 100;

        GameObject textContainer = new GameObject();
        textContainer.name = "Text";
        textContainer.transform.SetParent(label.transform);
        textContainer.AddComponent<Text>();
        Text text = textContainer.GetComponent<Text>();

        text.font = fontArial;
        text.fontSize = 14;
        text.lineSpacing = 1f;
        text.alignment = TextAnchor.MiddleCenter;
        text.text = item.label + " (Available: " + item.amount + ")" ;

        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0f, -0.6f, 0);
        rectTransform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

        return label;
    }
}
