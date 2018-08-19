using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets._2D;


[RequireComponent(typeof(Platformer2DUserControl))]
[RequireComponent(typeof(Weapon))]
public class Player : MonoBehaviour {

    public int fallBoundary = -20;
    public Transform deathParticles;

    // cache
    private AudioManager audioManager;

    public string grunt = "Grunt1";
    public string playerHit = "PlayerHit";

    [SerializeField]
    private StatusIndicator statusIndicator;

    public PlayerStats stats;

    private void Start()
    {
        stats = PlayerStats.instance;

        stats.curHealth = stats.maxHealth;

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

        GameMaster.gameMaster.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        InvokeRepeating("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);
    }

    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    private void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(999999);
        }
    }

    private void OnUpgradeMenuToggle(bool active)
    {
        GetComponent<Platformer2DUserControl>().enabled = !active;
        GetComponent<Weapon>().enabled = !active;
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if(stats.curHealth > 0)
            audioManager.PlaySound(playerHit);
        
        if(stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        GetComponent<Platformer2DUserControl>().Blink(2);
    }

    private void OnDestroy()
    {
        GameMaster.gameMaster.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}
