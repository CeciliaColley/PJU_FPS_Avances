using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioClip arenaClip;
    [SerializeField] private AudioClip normalClip;
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private float endVolume = 0.01f;
    private bool isInArena;
    private bool wateredPlants = false;

    void Start()
    {
        audioSource1.clip = normalClip;
        audioSource2.clip = arenaClip;

        audioSource1.volume = 0f;
        audioSource2.volume = 0f;

        audioSource1.Play();
        audioSource2.Play();

        StartCoroutine(FadeIn(audioSource1));
    }

    void Update()
    {
        if (PlayerState.Instance.isInArena != isInArena)
        {
            isInArena = PlayerState.Instance.isInArena;
            if (isInArena)
            {
                StartCoroutine(SwitchTrack(audioSource1, audioSource2));
            }
            else if (!isInArena && !wateredPlants)
            {
                StartCoroutine(SwitchTrack(audioSource2, audioSource1));
            }
        }
        if (PlayerState.Instance.wateredPlants != wateredPlants)
        {
            wateredPlants = PlayerState.Instance.wateredPlants;
            if (wateredPlants)
            {
                StartCoroutine(SwitchTrack(audioSource2, audioSource1));
            }
        }
    }

    private IEnumerator SwitchTrack(AudioSource fromSource, AudioSource toSource)
    {
        yield return StartCoroutine(FadeOut(fromSource));
        yield return StartCoroutine(FadeIn(toSource));
    }

    private IEnumerator FadeIn(AudioSource audioSource)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;
        float elapsedTime = 0f;
        
        audioSource.Play();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        audioSource.volume = endVolume;
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
