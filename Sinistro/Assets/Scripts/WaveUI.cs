﻿using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;

    // Use this for initialization
	void Start () 
    {
        if(spawner == null)
        {
            Debug.LogError("No spawner referenced");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced");
            this.enabled = false;
        }
        if (waveCountdownText == null)
        {
            Debug.LogError("No waveCountdown referenced");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogError("No waveCount referenced");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch(spawner.State)
        {
            case WaveSpawner.SpawnState.Counting:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.Spawning:
                UpdateSpawnUI();
                break;
        }
        previousState = spawner.State;
	}

    void UpdateCountingUI()
    {
        if(previousState != WaveSpawner.SpawnState.Counting)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }

    void UpdateSpawnUI()
    {
        if (previousState != WaveSpawner.SpawnState.Spawning)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.Count.ToString();
        }
    }
}
