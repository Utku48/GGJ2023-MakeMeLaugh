using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBoxAnim : MonoBehaviour, Interactable
{
    [SerializeField] Animator _supriseBoxAnimator;
    [SerializeField] ParticleSystem _suprisedParticle;
    [SerializeField] AudioSource _confetti;
    public void interact()
    {
        Debug.Log("box");
        _supriseBoxAnimator.SetBool("suprise", true);
        _suprisedParticle.Play();

        AudioSourceManager.Instance._sounds[0].Play();
    }
}
