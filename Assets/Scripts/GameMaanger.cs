using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _gameSound;
    [SerializeField] private AudioSource[] _screamSmile;
    float a;

    void Start()
    {
        _gameSound.Play();
        // StartCoroutine(UpdateAValue());
    }

    IEnumerator UpdateAValue()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

         
            a = Random.Range(0f, 3f);

           
            int index = Mathf.FloorToInt(a); 

            if (index >= 0 && index < _screamSmile.Length)
            {
                
                _screamSmile[index].Play();
            }
            else
            {
              
                Debug.LogError("Geçersiz indeks: " + index);
            }
        }
    }
}
