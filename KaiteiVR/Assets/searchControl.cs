using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchControl : MonoBehaviour    
{
    private bool scaleSwitch = true;
    void Start()
    {
        scaleSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            scaleSwitch = false;

            StartCoroutine("ScaleUp");
        }

  
    }

    IEnumerator ScaleUp()
    {
        for (float i = 1; i < 200; i += 0.3f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.01f);
        }

        scaleSwitch = true;

        if (scaleSwitch)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        
    }
}
