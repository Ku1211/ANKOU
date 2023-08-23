using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabitemtrigger : MonoBehaviour
{
    public static Grabitemtrigger instance;
    public bool Grubjudg;

    public void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Grubjudg = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        Grubjudg = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        Grubjudg = false;
    }
}
