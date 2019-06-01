using UnityEngine;
using Valve.VR;

public class Intro : MonoBehaviour {

    private void StartGame ()
    {
        SteamVR_LoadLevel.Begin("Level_1");
    }
}
