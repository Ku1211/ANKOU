using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchHit : MonoBehaviour
{
    public GameObject outline;

    // Start is called before the first frame update
    void Start()
    {
        outline.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "search")
        {
            Invoke(nameof(DelayMethod), 5f);

            outline.gameObject.SetActive(true);
        }
    }

    void DelayMethod()
    {
        outline.gameObject.SetActive(false);
    }
}
