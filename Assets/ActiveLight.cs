using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour
{
    public GameObject light;
    public DetectionBehaviour detect;

    void Update()
    {
        if (detect.IsDetected)
        {
            light.SetActive(true);
        }
        else
        {
            light.SetActive(false);
        }
    }
}
