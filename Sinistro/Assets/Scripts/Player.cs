﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats 
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }

    }

    public PlayerStats stats = new PlayerStats();
    public int fallBoundary = -20;
    public Transform deathParticles;

    // cache
    private AudioManager audioManager;

    public string grunt = "Grunt1";
    public string playerHit = "PlayerHit";

    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        stats.Init();

        if(statusIndicator == null)
        {
            Debug.LogError("No status indicator on player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        // caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("PANIC!!! No AudioManager found!!!");
        }
    }

    private void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(999999);
        }
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        audioManager.PlaySound(playerHit);
        if(stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
}
