
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

public class Extinguisher : UdonSharpBehaviour
{
    public GameObject foam;
    public VRCPickup pickup;
    public string[] fireTypes;
    public float maxDistance = 10.0f;
    public Transform foamOrigin;

    public override void InputUse(bool value, UdonInputEventArgs args)
    {
        // Check if the current object is held
        if(!pickup.IsHeld) 
        {
            return;
        }

        foam.SetActive(value);
    }

    void Start()
    {
        
    }
}
