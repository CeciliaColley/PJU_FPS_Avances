using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlsInput : MonoBehaviour
{
    private IA_PlayerActions playerActions;
    [SerializeField] private ShowQuests showQuests;
    [SerializeField] private string sceneToLoad = "Menu";

    private void Awake()
    {
        playerActions = new IA_PlayerActions();
    }

    private void OnEnable()
    {
        playerActions.GameControls.Enable();

        playerActions.GameControls.GoToMainMenu.performed += GoToMainMenu;
        playerActions.GameControls.OpenQuestBook.performed += OpenQuestBook;
    }

    private void OnDisable()
    {
        playerActions.GameControls.Disable();

        playerActions.GameControls.GoToMainMenu.performed -= GoToMainMenu;
        playerActions.GameControls.OpenQuestBook.performed -= OpenQuestBook;
    }

    private void GoToMainMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OpenQuestBook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (showQuests != null)
        {
            showQuests.OpenQuestBook();
        }
        else
        {
            Debug.LogError("showQuests is not assigned.");
        }
    }
}