using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWhiteToFade : MonoBehaviour {

    [SerializeField]
    Image whiteImage;

    [SerializeField]
    AudioSource fadeSound;

    public float slowfactor = 255.0f;

    private void Start()
    {
        PlayFadeSound();
        StartCoroutine(WhiteToFade(0.0f, slowfactor));
    }

    public void PlayFadeSound()
    {
        fadeSound.Play();
    }

    public IEnumerator WhiteToFade(float delay, float slowfactor)
    {
        yield return new WaitForSeconds(delay);
        Color temp = whiteImage.color;
        temp.a = 1;
        while (true)
        {
            temp.a -= 1 / slowfactor;
            whiteImage.color = temp;
            if (temp.a <= 0)
            {
                temp.a = 0;
                whiteImage.color = temp;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
