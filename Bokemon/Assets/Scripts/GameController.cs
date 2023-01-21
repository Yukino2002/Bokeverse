using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    FreeRoam,
    Battle
}

public class GameController : MonoBehaviour {
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    
    GameState state;

    private void Start() {
        // set the game state to free roam
        state = GameState.FreeRoam;
        // subscribe to the OnEncounter event
        playerController.OnEncounter += StartBattle;
        // subscribe to the OnBattleOver event
        battleSystem.OnBattleOver += EndBattle;
    }

    private void StartBattle() {
        // set the game state to battle mode
        state = GameState.Battle;
        // as the battle system is initially disabled, enable it
        battleSystem.gameObject.SetActive(true);
        // disable our main camera, so that the battle camera can be used
        worldCamera.gameObject.SetActive(false);

        // reset and start a new battle
        battleSystem.StartBattle();
    }

    void EndBattle(bool won) {
        // set the game state to free roam
        state = GameState.FreeRoam;
        // disable the battle system
        battleSystem.gameObject.SetActive(false);
        // enable the main camera
        worldCamera.gameObject.SetActive(true);
    }

    private void Update() {
        if (state == GameState.FreeRoam) {
            playerController.HandleUpdate();
        } else if (state == GameState.Battle) {
            battleSystem.HandleUpdate();
        }
    }
}