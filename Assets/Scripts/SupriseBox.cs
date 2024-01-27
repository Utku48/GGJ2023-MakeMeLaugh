using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBox : MonoBehaviour, Interactable
{
    [SerializeField] private ParticleSystem _dust;
    public void interact()
    {
        gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 5f);

        if (gameObject.CompareTag("x"))
        {
            StartCoroutine(dustPlay());
        }


    }

    IEnumerator dustPlay()
    {
        yield return new WaitForSeconds(3f);

        _dust.Play();
    }


}
