using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBox : MonoBehaviour, Interactable
{
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private Animator _tokmakDoorAnim;
    public void interact()
    {
        if (gameObject.CompareTag("tokmakDoor"))
        {

            gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), .5f);
            _tokmakDoorAnim.SetBool("tokmak", true);
            StartCoroutine(tokmakAnim());
        }

        if (gameObject.CompareTag("x"))
        {
            gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 3.5f);
            StartCoroutine(dustPlay());
        }


    }

    IEnumerator dustPlay()
    {
        yield return new WaitForSeconds(3f);

        _dust.Play();
    }


    IEnumerator tokmakAnim()
    {
        yield return new WaitForSeconds(1f);

    }

}
