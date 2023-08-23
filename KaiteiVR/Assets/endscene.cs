using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscene : MonoBehaviour
{
    public Transform2 transform2;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = transform2.GetBattery;

        if (count == 4)
        {
            SceneManager.LoadScene("OC_ending");
        }
    }
}
