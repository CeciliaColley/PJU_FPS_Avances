using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
 To use the methods in this script, I reccomend that the game object this script is attached to has a collider set to trigger, and an 
 event trigger (likely with a pointer-click event type, but customize to your requirements) that references this script.
 */


public class QuestCatalyst : MonoBehaviour
{
    [SerializeField] private string questName;
    [SerializeField] private GameObject exclamation;
    private bool questAdded = false;
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            CreateQuest();
        }
    }

    public void CreateQuest()
    {
        if (questName != null && !questAdded) 
        {
            questAdded = !questAdded;
            PlayerState.Instance.questNames.Add(questName);
            exclamation.SetActive(true);
        }
        else if (questName == null) { Debug.LogWarning("No quest was added when interacting with " + gameObject.name + " because the field 'quest' in the QuestCatalyst script is empty. Please type in a valid quest to the 'quest' field in the inspector."); }

        if (exclamation != null && !questAdded)
        {
            exclamation.SetActive(true);
        } else if (exclamation == null) { Debug.LogWarning("The exclamation mark doesn't display when interacting with " + gameObject.name + " because the field 'exclamation' in the QuestCatalyst script is empty. Please reference the exclamation mark in the inspector."); }
    }

    public void CompleteQuest()
    {
        if (questName != null && PlayerState.Instance.questNames.Contains(questName))
        {
            PlayerState.Instance.questNames.Remove(questName);
        }
        else { Debug.LogWarning("No quest was removed when interacting with " + gameObject.name + ". This is because either the field 'quest' of the QuestCatalyst script is empty, or it doesn't match any of the active quests. Please assign a valid quest to the 'quest' field, or check that the quest you are trying to remove exists."); }
    }

    public void CompleteQuest(string quest)
    {
        if (PlayerState.Instance.questNames.Contains(quest))
        {
            PlayerState.Instance.questNames.Remove(quest);
        }
        else { Debug.LogWarning("No quest was removed when interacting with " + gameObject.name + ". This is because either the field 'quest' of the QuestCatalyst script is empty, or it doesn't match any of the active quests. Please assign a valid quest to the 'quest' field, or check that the quest you are trying to remove exists."); }
    }
}
