using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, Interactable
{
    private bool isOpen = false;
    public bool isTrapDoor;
    [SerializeField] private GameObject TrapWall;
    [SerializeField] private AudioSource _OpenDoorCreak;
    [SerializeField] private AudioSource _CloseDoorCreak;
    public void interact()
    {
        Sequence mySequence = DOTween.Sequence();
        if (!isOpen)
        {

            mySequence.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), 4f, RotateMode.LocalAxisAdd));
            isOpen = true;
            _OpenDoorCreak.Play();

            if (isTrapDoor)
            {
                mySequence.Append(TrapWall.transform.DOLocalMove(new Vector3(TrapWall.transform.localPosition.x, TrapWall.transform.localPosition.y, TrapWall.transform.localPosition.z - 2), .5f));
            }
        }
        else
        {

            mySequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), 1f, RotateMode.LocalAxisAdd));
            isOpen = false;
            _CloseDoorCreak.Play();
        }
        mySequence.Play();

        Debug.Log("interact");
    }
}
