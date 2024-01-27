using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBox : MonoBehaviour, Interactable
{
    public bool iron = false;
    public bool tokmak = false;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private Animator _tokmakDoorAnim;

    [SerializeField] private GameObject _tokmakDoor;
    [SerializeField] private GameObject _ironDoor;

    public void interact()
    {
        if (gameObject.CompareTag("tokmakDoor"))
        {

            tokmak = true;
            gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), 1.5f);
            _tokmakDoorAnim.SetBool("sallan", true);
            StartCoroutine(tokmakAnim());


        }

        if (gameObject.CompareTag("x"))
        {
            gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 3.5f);
            StartCoroutine(dustPlay());

        }

        if (gameObject.CompareTag("ironDoor"))
        {
            iron = true;
            gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 2.5f);
            Debug.Log("iron" + iron);
        }

        if (gameObject.CompareTag("leftIronDoor"))
        {
            if (_tokmakDoor.GetComponent<SupriseBox>().tokmak && _ironDoor.transform.GetChild(0).GetComponent<SupriseBox>().iron)
            {
                gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 2.5f);

            }
        }

    }

    IEnumerator dustPlay()
    {
        yield return new WaitForSeconds(3f);

        _dust.Play();

    }


    IEnumerator tokmakAnim()
    {
        yield return new WaitForSeconds(2f);
        _tokmakDoorAnim.SetBool("tokmak", true);
    }

}
