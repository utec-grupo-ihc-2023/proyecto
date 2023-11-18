
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
    public float extinguishRate = 0.5f;

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
            float deltaRate = Mathf.Pow(extinguishRate, Time.deltaTime);

            var main = fire.GetComponent<ParticleSystem>().main;

            main.startSize = new ParticleSystem.MinMaxCurve(main.startSize.constantMin * deltaRate, main.startSize.constantMax * deltaRate);

            if(main.startSize.constantMin < 0.1f)
            {
                fire.SetActive(false);
            }
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
