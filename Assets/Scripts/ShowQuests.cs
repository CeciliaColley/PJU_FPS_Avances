using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuests : MonoBehaviour
{
    [SerializeField] private GameObject questPage; // Please assign the open quest book UI element to this field in the inspector.
    [SerializeField] private Text questTextBox; // Please assign the text box where quests will be displayed to this field in the inspector.
    [SerializeField] private GameObject notification; // Please assign the noitification UI element to this field in the inspector.
    [SerializeField] private string[] noQuestsText; // Please add as many default text options as desired to this field in the inspector.

    
    private bool openBook;

    public void OpenQuestBook()
    {
        // Toggle the flag to open the book and run the create page and write quests methods respectively.
        openBook = !openBook;
        CreatePage();
        WriteQuests();
    }

    private void CreatePage()
    {
        // Display the open book UI element (and it's child text box) depending on the openBook flag state.
        if (questPage != null) 
        {
            if (openBook) 
            { 
                questPage.SetActive(true); 
                notification.SetActive(false); 
                gameObject.GetComponent<Button>().OnDeselect(null); 
            }
            else 
            { 
                questPage.SetActive(false); 
                gameObject.GetComponent<Button>().OnDeselect(null); 
            }
        }
        
        // Print warnings that will help user set up the project.
        else if (questPage == null) { Debug.LogWarning("The CreatePage method in the ShowQuests script that is attached to " + gameObject.name + " failed because 'Quest Page' is null. Please make sure a page UI element has been assigned to this field in the inspector."); }
        else if (notification == null) { Debug.LogWarning("The CreatePage method in the ShowQuests script that is attached to " + gameObject.name + " failes because 'exclamation' is null. Please make sure a exclamation UI element has been assigned to this field in the inspector."); }
    }

    private void WriteQuests()
    {
        if (questTextBox != null)
        {
            //Display default text if there are no active quests.
            if (MainManager.mainManager.questNames.Count == 0)
            {
                if (noQuestsText != null)
                {
                    int randomNumber = (Random.Range(0, noQuestsText.Length));
                    questTextBox.text = noQuestsText[randomNumber];
                }
                else Debug.LogWarning("No default text is showing because the 'No Quests Text' field in the ShowQuests script attached to " + gameObject.name + "is empty or null. Please add valid default text to this field in the inspector.");
            }
            else
            {
                // Use a string builder to write each quest on a new line in the text box atatched to this script.
                StringBuilder stringBuilder = new();
                foreach (string quest in MainManager.mainManager.questNames)
                {
                    stringBuilder.AppendLine(quest);
                }
                questTextBox.text = stringBuilder.ToString();
            }

            // Ensure the text box is the appropriate size.
            questTextBox.rectTransform.sizeDelta = new Vector2(questTextBox.rectTransform.sizeDelta.x, questTextBox.preferredHeight);
        }
        else { Debug.LogWarning("The WriteQuests method in the ShowQuests script that is attached to " + gameObject.name + " failed because 'Quest Text Box' is null. Please make sure a text box has been assigned to this field in the inspector."); }
        
    }
}
