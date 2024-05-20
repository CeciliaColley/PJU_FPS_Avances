using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Reference to the controls image GameObject
    [SerializeField] private GameObject controlsImage;
    [SerializeField] private string sceneToLoad = "Shamba";

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Method to be called when the Play button is clicked
    public void PlayGame()
    {
        // Load the scene called "Shamba"
        SceneManager.LoadScene(sceneToLoad);
    }

    // Method to be called when the Controls button is clicked
    public void ShowControls()
    {
        // Set the controlsImage GameObject as active
        controlsImage.SetActive(true);

    }

    public void CloseControls()
    {
        // Set the controlsImage GameObject as false
        controlsImage.SetActive(false);

    }

    // Method to be called when the Exit button is clicked
    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }
}