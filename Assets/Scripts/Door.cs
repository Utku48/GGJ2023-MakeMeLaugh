using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, Interactable
{
    private bool isOpen = false;
    public bool isTrapDoor;
    [SerializeField] private GameObject TrapWall;

    [SerializeField] private AudioSource _CloseDoorCreak;
    [SerializeField] private Vector3 trapDoorPos;
    public void interact()
    {
        Sequence mySequence = DOTween.Sequence();
        if (!isOpen)
        {


            isOpen = true;

            AudioSourceManager.Instance._sounds[2].Play();


            if (isTrapDoor)
            {
                mySequence.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), 2f, RotateMode.LocalAxisAdd));
                mySequence.Append(TrapWall.transform.DOLocalMove(trapDoorPos, .5f));
            }
            else
            {
                mySequence.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), 3f, RotateMode.LocalAxisAdd));
            }
        }
        else
        {

            mySequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), .75f, RotateMode.LocalAxisAdd));
            isOpen = false;
            AudioSourceManager.Instance._sounds[1].Play();
        }
        mySequence.Play();

        Debug.Log("interact");
    }
}
