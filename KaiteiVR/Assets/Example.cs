using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Transform m_target = null;
    public Transform s_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;
    public bool Torder;
    public bool grabmode = false;
    public Vector3 targetPosition = Vector3.zero;

    private Vector3 m_velocity;

    private void Start()
    {
        Torder = false;
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            m_velocity += (targetPosition - transform.position) * m_speed;
            m_velocity *= m_attenuation;
            transform.position += m_velocity *= Time.deltaTime;
        }

        else if((OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) || !(OVRInput.Get(OVRInput.RawButton.RHandTrigger)))
        {
            m_velocity += (s_target.position - transform.position) * m_speed;
            m_velocity *= m_attenuation;
            transform.position += m_velocity *= Time.deltaTime;
            Torder = false;
        }

    }
}