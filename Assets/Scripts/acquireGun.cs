using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class acquireGun : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject blockade;
    [SerializeField] private string questToComplete;
    private bool inCollider = false;
    private bool pressedE = false;

    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            inCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inCollider = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inCollider && !pressedE)
        {
            pressedE = true;
            PlayerState.Instance.hasGun = true;
            QuestCatalyst questCatalyst = GetComponent<QuestCatalyst>();
            if (questCatalyst != null)
            {
                questCatalyst.CompleteQuest();
                questCatalyst.CompleteQuest(questToComplete);
            }
            if (blockade != null)
            {
                blockade.SetActive(false);
            }
            AudioSource audioSource = GetComponent <AudioSource>();
            StartCoroutine(WaitForAudioToStop(audioSource));
            
        }
    }

    


    private IEnumerator WaitForAudioToStop(AudioSource audioSource)
    {
        audioSource.Play();
    
        // Wait until the audio has finished playing
        while (audioSource.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        Destroy(gun);
        Destroy(gameObject);
    }
}
