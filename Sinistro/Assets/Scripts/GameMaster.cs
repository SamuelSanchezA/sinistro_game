﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static GameMaster gameMaster;


    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    [SerializeField]
    private int startingMoney;
    public static int Money;

    private void Awake()
    {
        if(gameMaster == null)
        {
            gameMaster = this;
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float delay = 2f;
    public Transform spawnPrefab;
    public string mainSong;
    public string spawn;

    private bool canSpawn;

    public string gameOverSound = "GameOver";

    // Display lives left
    public Sprite[] liveSprites;
    public Image blueUI;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    // cache
    private AudioManager audioManager;

    void Start()
    {
        _remainingLives = maxLives;

        canSpawn = true;

        Money = startingMoney;

        // caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("PANIC!!! No AudioManager found!!!");
        }
        else
        {
            audioManager.PlaySound(mainSong);
            Debug.Log("Playing " + mainSong);
        }

        audioManager.StopSound("Homenaje");

    }

    private void Update()
    {
        blueUI.sprite = liveSprites[_remainingLives];
        if(Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    public void EndGame()
    {
        audioManager.PlaySound(gameOverSound);

        gameOverUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer()
    {
        canSpawn = false;
        yield return new WaitForSeconds(delay);
        audioManager.PlaySound(spawn);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);﻿
        canSpawn = true;
    }

    public static void KillPlayer(Player player)
    {
        Debug.Log("canSpawn: " + gameMaster.canSpawn);
        if (gameMaster.canSpawn)
        {
            gameMaster.canSpawn = false;
            gameMaster._KillPlayer(player);
        }

        return;
    }

    public void _KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        Transform clone = Instantiate(player.deathParticles, player.transform.position, Quaternion.identity);
        Destroy(clone.gameObject, 5f);
        audioManager.PlaySound(player.grunt);

        if (_remainingLives != 0)
            _remainingLives--;
        else
            _remainingLives = 0;
        
        Debug.Log("Lives left: " + _remainingLives);

        if (_remainingLives > 0)
        {
            StartCoroutine(gameMaster.RespawnPlayer());
        }
        else
        {
            blueUI.sprite = liveSprites[_remainingLives];
            gameMaster.EndGame();
            return;
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        gameMaster._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy enemy)
    {
        Transform clone = Instantiate(enemy.deathParticles, enemy.transform.position, Quaternion.identity);
        Destroy(clone.gameObject, 5f);
        Destroy(enemy.gameObject);

        audioManager.PlaySound(enemy.deathSound);
        audioManager.PlaySound(enemy.grunt);

        Money += enemy.moneyDrop;
        audioManager.PlaySound("GetItMoney");
    }
}