using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// different states that define the battle
public enum BattleState {
    Start, 
    PlayerAction, 
    PlayerMove, 
    EnemMove, 
    Busy
}

// main battle system
public class BattleSystem : MonoBehaviour {
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    // create instance for battle state
    BattleState state;

    int currentAction;
    int currentMove;

    private void Start() {
        StartCoroutine(SetupBattle());
    }

    // the initial coroutine to display dialog and switch to player action state
    public IEnumerator SetupBattle() {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Bokemon);
        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Bokemon);

        // set the moves of the player unit
        dialogBox.SetMoveNames(playerUnit.Bokemon.Moves);

        // waits for the coroutine to get completed
        yield return dialogBox.TypeDialog("   A wild " + enemyUnit.Bokemon.Base.Name + " appeared!");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    // the series of events that happen during the player action state
    public void PlayerAction() {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("   Choose an action."));
        dialogBox.EnableActionSelector(true);
    }

    // the series of events that happen during the player move state
    public void PlayerMove() {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    private void Update() {
        if (state == BattleState.PlayerAction) {
            HandleActionSelector();
        } else if (state == BattleState.PlayerMove) {
            HandleMoveSelection();
        }
    }

    // fight text is at index 0, run text is at index 1
    void HandleActionSelector() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentAction < 1) {
                currentAction += 1;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentAction > 0) {
                currentAction -= 1;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z)) {
            // fight
            if (currentAction == 0) {
                PlayerMove();
            } else if (currentAction == 1) {
                // run
            }
        }
    }

    void HandleMoveSelection() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (currentMove < playerUnit.Bokemon.Moves.Count - 1) {
                currentMove += 1;
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (currentMove > 0) {
                currentMove -= 1;
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentMove < playerUnit.Bokemon.Moves.Count - 2) {
                currentMove += 2;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentMove > 1) {
                currentMove -= 2;
            }
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Bokemon.Moves[currentMove]);
    }
}
