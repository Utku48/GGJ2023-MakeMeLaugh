using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseActivator : MonoBehaviour
{
    [SerializeField] private bool isReverse;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if(isReverse)
                playerController.isReversed = true;
            else
                playerController.isReversed = false;
        }
    }
}
