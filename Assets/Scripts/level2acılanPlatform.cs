using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class level2acılanPlatform : MonoBehaviour
{
    [SerializeField] GameObject leftFloor;
    [SerializeField] GameObject rightFloor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            AudioSourceManager.Instance._sounds[9].Play();


            leftFloor.transform.DOMove(new Vector3(43, 0, -15), 1f);
            rightFloor.transform.DOMove(new Vector3(36, 0, -28), 1f);
        }
    }
}
