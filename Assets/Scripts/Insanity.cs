using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;


public class Insanity : MonoBehaviour
{
    public float insanity_amount = 100;
    public float insanity_timer_multipiler = 1;
    public Image insanity_image;
    public Volume volume;
    Vignette vignette;
    FilmGrain grain;
    ChromaticAberration aberration;
    ColorAdjustments colorGrading;
    VolumeProfile VolumeProfile;
    LensDistortion lensDistortion;
    
    private void Start()
    {
        if(volume.profile.TryGet<Vignette>(out Vignette vignettee)){
            vignette = vignettee;
        }
        if (volume.profile.TryGet<FilmGrain>(out FilmGrain grainout))
        {
            grain = grainout;
        }
        if (volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberration))
        {
            aberration = chromaticAberration;
        }
        if (volume.profile.TryGet<LensDistortion>(out LensDistortion lensDistortiont))
        {
            lensDistortion = lensDistortiont;
        }

    }
    private void Update()
    {
        
        insanity_amount -= Time.deltaTime* insanity_timer_multipiler;
        insanity_image.fillAmount = insanity_amount/100;
        if(insanity_amount >0)
        {

            vignette.intensity.value = 1-(insanity_amount/100);
            grain.intensity.value = 1 - (insanity_amount / 100);
            aberration.intensity.value = 1 - (insanity_amount / 100);
            lensDistortion.intensity.value = 1 - (insanity_amount / 100);   
        }
        if (insanity_amount <= 0)
        {
            PlayerController.Instance.Die();
        }
    }
}
