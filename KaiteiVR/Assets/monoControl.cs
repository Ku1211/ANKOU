using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class monoControl : MonoBehaviour
{
    [SerializeField]
    private Volume GlobalVolumeProfile;

    private ColorAdjustments COA;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVolumeProfile.profile.TryGet(out COA);
    }


    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            COA.saturation.value = -100f;
            StartCoroutine("SaturationSetup");
            COA.contrast.value = -60f;
            StartCoroutine("ContrastSetup");
        }
       }
    // Update is called once per frame
    IEnumerator ContrastSetup()
    {
        float i;
            for ( i = -60f; i < 1f; i++)
            {
                //Debug.Log("FOR CHECK");
                COA.contrast.value = i;
            yield return new WaitForSeconds(0.2f);
             }
    }

    IEnumerator SaturationSetup()
    {
        float i;
        for (i = -100f; i < 1f; i++)
        {
            //Debug.Log("FOR CHECK");
            COA.saturation.value = i;
            yield return new WaitForSeconds(0.2f);
        }
    }
}