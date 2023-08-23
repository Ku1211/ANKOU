using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControlle : MonoBehaviour
{
    public Transform p_target = null;
    public float p_speed = 5;
    public float p_attenuation = 0.5f;

    private Vector3 p_velocity;

    private void Update()
    {
        if ((!OVRInput.Get(OVRInput.RawButton.RHandTrigger)) || (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)))
        {
            p_velocity += (p_target.position - transform.position) * p_speed;
            p_velocity *= p_attenuation;
            transform.position += p_velocity *= Time.deltaTime;
        }
    }
}