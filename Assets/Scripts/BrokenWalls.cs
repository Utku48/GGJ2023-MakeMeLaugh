using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BrokenWalls : MonoBehaviour
{

    [SerializeField] private GameObject _brokenWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Sa");
            RotateContinuously();
        }

    }

    void RotateContinuously()
    {

        _brokenWall.transform.DORotate(new Vector3(0f, 0f, 40f), 1.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => _brokenWall.transform.DORotate(new Vector3(0f, 0f, -40f), 1.5f));
        StartCoroutine(BrokeBrick());
    }

    IEnumerator BrokeBrick()
    {
        yield return new WaitForSeconds(2.5f);

        _brokenWall.transform.DOMove(Vector3.zero, .1f);
        _brokenWall.transform.DOScale(Vector3.zero, .1f);
    }
}

