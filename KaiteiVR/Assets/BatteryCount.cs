using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCount : MonoBehaviour
{
    public int count;

    public Transform2 transform2;

    public GameObject battery0;
    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;
    public GameObject battery4;
    public GameObject battery5;
    public GameObject battery6;

    private bool On1 = false;
    private bool On2 = false;
    private bool On3 = false;
    private bool On4 = false;
    private bool On5 = false;
    private bool On6 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = transform2.GetBattery;

        if (count == 1 && (On1 == false))
        {
            battery0.gameObject.SetActive(false);
            battery1.gameObject.SetActive(true);
            On1 = true;
            //Debug.LogError("case1");
        }

        if (count == 2 && (On2 == false))
        {
            battery1.gameObject.SetActive(false);
            battery2.gameObject.SetActive(true);
            On2 = true;
            //Debug.LogError("case2");
        }

        if (count == 3 && (On3 == false))
        {
            battery2.gameObject.SetActive(false);
            battery3.gameObject.SetActive(true);
            On3 = true;
            //Debug.LogError("case3");
        }

        if (count == 4 && (On4 == false))
        {
            battery3.gameObject.SetActive(false);
            battery4.gameObject.SetActive(true);
            On4 = true;
            //Debug.LogError("case4");
        }

        if (count == 5 && (On5 == false))
        {
            battery4.gameObject.SetActive(false);
            battery5.gameObject.SetActive(true);
            On5 = true;
            //Debug.LogError("case5");
        }

        if (count == 6 && (On6 == false))
        {
            battery5.gameObject.SetActive(false);
            battery6.gameObject.SetActive(true);
            On6 = true;
            //Debug.LogError("case6");
        }
    }
}
