using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool m_IsDetected = false;

    public bool IsDetected
    {
        get
        {
            return m_IsDetected;
        }

        set
        {
            m_IsDetected = value;
        }
    }
}
