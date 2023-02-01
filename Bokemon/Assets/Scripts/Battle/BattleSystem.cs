using System;
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
    Busy,
    PartyScreen
}

// main battle system
public class BattleSystem : MonoBehaviour {
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;

    // event to trigger the end of the battle
    public event Action<bool> OnBattleOver;

    // create instance for battle state
    BattleState state;

    // variables to highlight the selected action and move
    int currentAction;
    int currentMove;
    int currentPartyMember;

    // variables to store the player party and wild bokemon
    BokemonParty playerParty;
    Bokemon wildBokemon;

    // Start is called before the first frame update
    public void StartBattle(BokemonParty playerParty, Bokemon wildBokemon) {
        this.playerParty = playerParty;
        this.wildBokemon = wildBokemon;
        StartCoroutine(SetupBattle());
    }

    // the initial coroutine to display dialog and switch to player action state
    public IEnumerator SetupBattle() {
        playerUnit.Setup(playerParty.GetHealthyBokemon());
        playerHud.SetData(playerUnit.Bokemon);
        enemyUnit.Setup(wildBokemon);
        enemyHud.SetData(enemyUnit.Bokemon);

        partyScreen.Init();

        // set the moves of the player unit
        dialogBox.SetMoveNames(playerUnit.Bokemon.Moves);

        // waits for the coroutine to get completed
        yield return dialogBox.TypeDialog("   A wild " + enemyUnit.Bokemon.Base.Name + " appeared!");

        PlayerAction();
    }

    // the series of events that happen during the player action state
    public void PlayerAction() {
        state = BattleState.PlayerAction;
        dialogBox.SetDialog("   Choose an action.");
        dialogBox.EnableActionSelector(true);
    }

    void openPartyScreen() {
        state = BattleState.PartyScreen;
        partyScreen.SetPartyData(playerParty.Bokemons);
        partyScreen.gameObject.SetActive(true);
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
        move.PP--;
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

            // end the battle
            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
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
        move.PP--;
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

            // end the battle
            yield return new WaitForSeconds(2f);

            // check if the player has any healthy bokemon
            var nextBokemon = playerParty.GetHealthyBokemon();
            if (nextBokemon != null) {
                openPartyScreen();
            } else {
                // if no, end the battle
                OnBattleOver(false);
            }
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
    public void HandleUpdate() {
        // if the state is player action, handle the action selector
        if (state == BattleState.PlayerAction) {
            // this displays the action selector component with fight and run
            HandleActionSelector();
        } else if (state == BattleState.PlayerMove) {
            // this displays the move selector component with the moves of the player unit
            HandleMoveSelection();
        } else if (state == BattleState.PartyScreen) {
            // this displays the party screen component with the bokemon of the player
            HandlePartySelection();
        }
    }

    // fight text is at index 0, run text is at index 1
    void HandleActionSelector() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // if down arrow is pressed, increment the current action
            currentAction += 2;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            // if up arrow is pressed, decrement the current action
            currentAction -= 2;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // if right arrow is pressed, increment the current action by 2
            currentAction += 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // if left arrow is pressed, decrement the current action by 2
            currentAction -= 1;
        }

        // clamp the current action to 0, 1, 2
        currentAction = Mathf.Clamp(currentAction, 0, 2);

        // highlights the selected action
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z)) {
            // if fight is selected, start the player move phase
            if (currentAction == 0) {
                PlayerMove();
            } else if (currentAction == 1) {
                openPartyScreen();
            } else if (currentAction == 2) {
                StartCoroutine(HandleRun());
            }
        }
    }

    // function to run from wild pokemon
    IEnumerator HandleRun(){
        yield return dialogBox.TypeDialog("   You decided to flee!");
        OnBattleOver(true);
    }

    void HandleMoveSelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // if down arrow is pressed, increment the current move
            currentMove += 2;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            // if up arrow is pressed, decrement the current move
            currentMove -= 2;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // if right arrow is pressed, increment the current move by 2
            currentMove += 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // if left arrow is pressed, decrement the current move by 2
            currentMove -= 1;
        }

        currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Bokemon.Moves.Count - 1);

        // highlights the selected move
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Bokemon.Moves[currentMove]);

        // once the move is selected, remove the move selector component and display the dialog text
        if (Input.GetKeyDown(KeyCode.Z)) {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        } else if (Input.GetKeyDown(KeyCode.X)) {
            // if x is pressed, remove the move selector component and display the action selector component
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            PlayerAction();
        }
    }

    void HandlePartySelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentPartyMember += 2;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentPartyMember -= 2;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentPartyMember += 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentPartyMember -= 1;
        }

        currentPartyMember = Mathf.Clamp(currentPartyMember, 0, playerParty.Bokemons.Count - 1);

        // highlights the selected bokemon
        partyScreen.UpdateMemberSelection(currentPartyMember);

        if (Input.GetKeyDown(KeyCode.Z)) {
            var selectedBokemon = playerParty.Bokemons[currentPartyMember];
            if (selectedBokemon.HP <= 0) {
                partyScreen.SetMessageText("Can't send out a fainted Bokemon!");
                return;
            }
            if (selectedBokemon == playerUnit.Bokemon) {
                partyScreen.SetMessageText("That Bokemon is already out!");
                return;
            }

            partyScreen.gameObject.SetActive(false);
            state = BattleState.Busy;
            StartCoroutine(SwitchBokemon(selectedBokemon));

        } else if (Input.GetKeyDown(KeyCode.X)) {
            partyScreen.gameObject.SetActive(false);
            PlayerAction();
        }
    }

    // Coroutine to switch the bokemon
    IEnumerator SwitchBokemon(Bokemon newBokemon) {
        if (playerUnit.Bokemon.HP > 0) {
            yield return dialogBox.TypeDialog("   Come back, " + playerUnit.Bokemon.Base.Name + "!");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
        }

        playerUnit.Setup(newBokemon);
        playerHud.SetData(newBokemon);
        dialogBox.SetMoveNames(newBokemon.Moves);

        yield return dialogBox.TypeDialog("   " + newBokemon.Base.Name + " is sent out!");

        StartCoroutine(PerformEnemyMove());
    }
}
