using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBoxAnim : MonoBehaviour, Interactable
{
    [SerializeField] Animator _supriseBoxAnimator;

    public void interact()
    {
        Debug.Log("box");
        _supriseBoxAnimator.SetBool("suprise", true);
    }
}
