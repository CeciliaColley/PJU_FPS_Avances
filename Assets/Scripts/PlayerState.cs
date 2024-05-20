using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private GameObject waterCanvas;
    [SerializeField] private bool canFish = false;
    [SerializeField] private MoveToLocation blockade;
    [SerializeField] private string loseMessage;
    [SerializeField] private GameObject notification;


    public bool hasGun = false;
    public List<string> questNames = new();
    public bool isInArena = false;
    public static PlayerState Instance;
    public float health;
    public float maxHealth = 200.0f;
    public bool wateredPlants = false;

    private FPSController player;
    private Vector3 resetPlayerPos;

    private void Awake()
    {
        health = maxHealth;

        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = FindAnyObjectByType<FPSController>();
        resetPlayerPos = player.transform.position;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            ResetGame();
        }
    }

    private void ResetGame()
    {
        isInArena = false;
        blockade = FindAnyObjectByType<MoveToLocation>();
        if (blockade != null)
        {
            Destroy(blockade.gameObject);
        }
        if (player != null)
        {
            // Disable FPSController script
            player.enabled = false;

            // Disable CharacterController component
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            //move player
            player.transform.position = resetPlayerPos;

            // Re-enable CharacterController component
            if (characterController != null)
            {
                characterController.enabled = true;
            }

            // Re-enable FPSController script
            player.enabled = true;
        }
        health = maxHealth;
        waterCanvas.SetActive(false);
        if (!questNames.Contains(loseMessage))
        {
            questNames.Add(loseMessage);
        }
        if (notification != null)
        {
            notification.SetActive(true);
        }
    }

    
}
