using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundEffects : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverButtonSound;
    [SerializeField] private AudioClip buttonClickSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHoverButtonSound()
    {
        audioSource.clip = hoverButtonSound;
        audioSource.Play();
    }

    public void PlayButtonClickSound()
    {
        audioSource.clip = buttonClickSound;
        audioSource.Play();
    }
}
