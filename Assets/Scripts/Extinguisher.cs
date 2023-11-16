
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

    bool sendRaycast = false;

    public override void InputUse(bool value, UdonInputEventArgs args)
    {
        // Check if the current object is held
        if(!pickup.IsHeld) 
        {
            return;
        }

        foam.SetActive(value);
        sendRaycast = value;
    }

    void Start()
    {
        
    }

    bool IsCompatible(GameObject fire)
    {
        for(int i = 0; i < fireTypes.Length; i++)
        {
            if(fireTypes[i] == fire.name)
            {
                return true;
            }
        }

        return false;
    }

    void Extinguish(GameObject fire)
    {
        if(IsCompatible(fire))
        {
            fire.SetActive(false);
        }
    }

    void SendRaycast()
    {
        Ray ray = new Ray(foamOrigin.position, foamOrigin.TransformDirection(Vector3.right));

        if(Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance))
        {
            Debug.DrawRay(ray.origin, hitInfo.point);

            Extinguish(hitInfo.transform.gameObject);
        }
    }

    void Update()
    {
        if(sendRaycast)
        {
            SendRaycast();
        }
    }
}
