using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBox : MonoBehaviour, Interactable
{
    [SerializeField] private AudioSource _flyBox;
    public void interact()
    {
        gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 5f);
        _flyBox.Play();

    }
}
