using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontlightContorol : MonoBehaviour
{
    public bool lightContorol = false;
    public GameObject frontLight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.X))
            {
            if (lightContorol == true)
            {
                lightContorol = false;
                frontLight.gameObject.SetActive(true);
                //Debug.LogError("aaa");
            }

            else
            {
                lightContorol = true;
                frontLight.gameObject.SetActive(false);
                //Debug.LogError("bbb");
            }
        }
    }
}
