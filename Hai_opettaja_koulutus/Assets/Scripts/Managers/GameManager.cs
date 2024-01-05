using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    private Master controls;
    public PlayerController playerController { get; set;}
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new Master();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (controls.Game.Pause.triggered)
        {
            if(IsGamePlay())
            {
                ChangeGameState(GameState.Pause);
            }
            else
            {
                ChangeGameState(GameState.Gameplay);
            }
        }
    }

    public bool IsGamePlay()
    {
        return currentState == GameState.Gameplay;
    }

    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
    }
}
