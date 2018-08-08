using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static GameMaster gameMaster;


    //[SerializeField]
    //private int maxLives = 3;
    //private static int _remainingLives;
    //public static int RemainingLives
    //{
    //    get { return _remainingLives; }
    //}

    private void Awake()
    {
        if(gameMaster == null)
        {
            gameMaster = this;
        }
        //GetComponent<Audio    Source>().Play();
    }

    void Start()
    {
        //_remainingLives = maxLives;
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float delay = 2;
    public Transform spawnPrefab;

    // Display lives left
    public Sprite[] liveSprites;
    public Image blueUI;

    [SerializeField]
    private GameObject gameOverUI;

    //private void Update()
    //{
    //    blueUI.sprite = liveSprites[_remainingLives];
    //}




    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);﻿
    }

    public static void KillPlayer(Player player)
    {
        gameMaster._KillPlayer(player);
    }

    public void _KillPlayer(Player player)
    {
        Transform clone = Instantiate(player.deathParticles, player.transform.position, Quaternion.identity);
        Destroy(clone.gameObject, 5f);
        Destroy(player.gameObject);
        //_remainingLives--;
        //Debug.Log("Lives left: " + _remainingLives); 
        //if (_remainingLives <= 0)
        //{
        //    gameOverUI.SetActive(true);
        //}
        //else
        //{

        gameMaster.StartCoroutine(gameMaster.RespawnPlayer());
        //}

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
    }
}
