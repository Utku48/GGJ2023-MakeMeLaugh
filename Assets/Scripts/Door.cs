using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, Interactable
{
    private bool isOpen=false;
    public void interact()
    {
        if (!isOpen)
        {
            transform.DORotate(new Vector3(0, -90, 0), 1f);
            isOpen=true;
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, 0), 1f);
            isOpen = false;
        }
           
        Debug.Log("interact");
    }
}
