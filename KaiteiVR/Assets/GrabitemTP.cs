using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabitemTP : MonoBehaviour
{
    public GameObject armP;
    public void GrabTeleport()
    {
        transform.position = armP.transform.position;
    }
}
