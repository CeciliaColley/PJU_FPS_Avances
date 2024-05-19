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
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 resetPlayerPos;
    [SerializeField] private string loseMessage;

    public bool hasGun = false;
    public List<string> questNames = new();
    public bool isInArena = false;
    public static PlayerState Instance;
    public float health;
    public float maxHealth = 200.0f;

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

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health < 0)
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
        player.transform.position = resetPlayerPos;
        health = maxHealth;
        waterCanvas.SetActive(false);
        if (!questNames.Contains(loseMessage))
        {
            questNames.Add(loseMessage);
        }
    }
}
