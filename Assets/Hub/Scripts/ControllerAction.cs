using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ControllerAction : MonoBehaviour
{
// a reference to the action
public SteamVR_Action_Boolean MenuOnOff;
// a reference to the hand
public SteamVR_Input_Sources handType;
//reference to the sphere
public GameObject menuCanvas;
    void Start()
{
    MenuOnOff.AddOnStateDownListener(ButtonDown, handType);
    MenuOnOff.AddOnStateUpListener(ButtonUp, handType);
    menuCanvas.SetActive(false);
}
public void ButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
{
    Debug.Log("Button released");
    menuCanvas.SetActive(false);
}
public void ButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
{
    Debug.Log("Button is Pressed");
    menuCanvas.SetActive(true);
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
