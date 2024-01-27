using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelEntry : MonoBehaviour
{
    private BoxCollider m_BoxCollider;
    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        m_BoxCollider.enabled = false;
        Debug.Log("first level riddle cýkýcak");
    }
}
