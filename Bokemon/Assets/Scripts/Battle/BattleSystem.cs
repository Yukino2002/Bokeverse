using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// different states that define the battle
public enum BattleState {
    Start, 
    PlayerAction, 
    PlayerMove, 
    EnemyMove, 
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

    // Start is called before the first frame update
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

    // coroutine to perform the player move
    IEnumerator PerformPlayerMove() {
        state = BattleState.Busy;

        // get the move from the player unit
        var move = playerUnit.Bokemon.Moves[currentMove];
        // display the move name
        yield return dialogBox.TypeDialog("   " + playerUnit.Bokemon.Base.Name + " used " + move.Base.Name + "!");

        // use the attack animation
        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        // perform the move
        var damageDetails = enemyUnit.Bokemon.TakeDamage(move, playerUnit.Bokemon);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted) {
            yield return dialogBox.TypeDialog("   " + enemyUnit.Bokemon.Base.Name + " fainted!");
            enemyUnit.PlayFaintAnimation();
        } else {
            // if not fainted, start the enemy move phase
            StartCoroutine(PerformEnemyMove());
        }
    }

    // coroutine to perform the enemy move
    IEnumerator PerformEnemyMove() {
        state = BattleState.EnemyMove;

        // get a random move from the enemy unit
        var move = enemyUnit.Bokemon.GetRandomMove();
        // display the move name
        yield return dialogBox.TypeDialog("   " + enemyUnit.Bokemon.Base.Name + " used " + move.Base.Name + "!");

        // display the attack animation
        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();
        
        // perform the move
        var damageDetails = playerUnit.Bokemon.TakeDamage(move, enemyUnit.Bokemon);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted) {
            yield return dialogBox.TypeDialog("   " + playerUnit.Bokemon.Base.Name + " fainted!");
            playerUnit.PlayFaintAnimation();
        } else {
            // if not fainted, start the player action phase cycle again
            PlayerAction();
        }        
    }

    // to show all the damage details in the dialog box
    IEnumerator ShowDamageDetails(DamageDetails damageDetails) {
        if (damageDetails.Critical > 1f) {
            yield return dialogBox.TypeDialog("   A critical hit!");
        }

        if (damageDetails.TypeEffectiveness > 1f) {
            yield return dialogBox.TypeDialog("   It's super effective!");
        } else if (damageDetails.TypeEffectiveness < 1f) {
            yield return dialogBox.TypeDialog("   It's not very effective...");
        }
    }

    // Update is called once per frame
    private void Update() {
        // if the state is player action, handle the action selector
        if (state == BattleState.PlayerAction) {
            // this displays the action selector component with fight and run
            HandleActionSelector();
        } else if (state == BattleState.PlayerMove) {
            // this displays the move selector component with the moves of the player unit
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

        // highlights the selected action
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z)) {
            // if fight is selected, start the player move phase
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

        // highlights the selected move
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Bokemon.Moves[currentMove]);

        // once the move is selected, remove the move selector component and display the dialog text
        if (Input.GetKeyDown(KeyCode.Z)) {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
