using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayManager : MonoBehaviour
{
    [SerializeField] GameObject IntroText;
 
    private float timer = 9f;

    private void Start()
    {
      
    }
    private void Update()
    {


        timer -= Time.deltaTime;
        if (timer < 0)
            IntroText.SetActive(true);
        if (Input.GetKeyUp(KeyCode.P))
        {
            SceneManager.LoadScene(1);
        }

    }
}
