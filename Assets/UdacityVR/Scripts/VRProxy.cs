using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;


public class VRProxy : MonoBehaviour {

    public SteamVR_Behaviour_PoseEvent onRightHandTransformChanged;
    public SteamVR_Behaviour_PoseEvent onRightHandTransformUpdated;
    public SteamVR_Behaviour_Pose_ConnectedChangedEvent onRightHandConnectedChanged;
    public SteamVR_Behaviour_Pose_TrackingChangedEvent onRightHandTrackingChanged;
    public SteamVR_Behaviour_Pose_DeviceIndexChangedEvent onRightHandDeviceIndexChanged;

    public SteamVR_Behaviour_PoseEvent onLeftHandTransformChanged;
    public SteamVR_Behaviour_PoseEvent onLeftHandTransformUpdated;
    public SteamVR_Behaviour_Pose_ConnectedChangedEvent onLeftHandConnectedChanged;
    public SteamVR_Behaviour_Pose_TrackingChangedEvent onLeftHandTrackingChanged;
    public SteamVR_Behaviour_Pose_DeviceIndexChangedEvent onLeftHandDeviceIndexChanged;

    private GameObject VRCamera;

    void Start () {
        GameObject player = GameObject.FindWithTag("Player").gameObject;
        GameObject SteamVRObjects = player.transform.Find("SteamVRObjects").gameObject;
        GameObject rightHand = SteamVRObjects.transform.Find("RightHand").gameObject;
        GameObject leftHand = SteamVRObjects.transform.Find("LeftHand").gameObject;
        VRCamera = SteamVRObjects.transform.Find("VRCamera").gameObject;

        AddRightHandListeners(rightHand);
        AddLeftHandListeners(leftHand);
    }

    public Vector3 GetCameraPosition() {
        return VRCamera.transform.position;
    }

    private void AddRightHandListeners(GameObject hand) {
        SteamVR_Behaviour_Pose pose = hand.GetComponent<SteamVR_Behaviour_Pose>();
        pose.onTransformChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource) => onRightHandTransformChanged.Invoke(fromAction, fromSource));
        pose.onTransformUpdated.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource) => onRightHandTransformUpdated.Invoke(fromAction, fromSource));
        pose.onConnectedChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, bool val) => onRightHandConnectedChanged.Invoke(fromAction, fromSource, val));
        pose.onTrackingChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, ETrackingResult result) => onRightHandTrackingChanged.Invoke(fromAction, fromSource, result));
        pose.onDeviceIndexChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, int result) => onRightHandDeviceIndexChanged.Invoke(fromAction, fromSource, result));
    }

    private void AddLeftHandListeners(GameObject hand) {
        SteamVR_Behaviour_Pose pose = hand.GetComponent<SteamVR_Behaviour_Pose>();
        pose.onTransformChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource) => onLeftHandTransformChanged.Invoke(fromAction, fromSource));
        pose.onTransformUpdated.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource) => onLeftHandTransformUpdated.Invoke(fromAction, fromSource));
        pose.onConnectedChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, bool val) => onLeftHandConnectedChanged.Invoke(fromAction, fromSource, val));
        pose.onTrackingChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, ETrackingResult result) => onLeftHandTrackingChanged.Invoke(fromAction, fromSource, result));
        pose.onDeviceIndexChanged.AddListener((SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, int result) => onLeftHandDeviceIndexChanged.Invoke(fromAction, fromSource, result));
    }

}
