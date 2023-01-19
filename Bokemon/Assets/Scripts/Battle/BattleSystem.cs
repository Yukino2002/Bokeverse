using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// main battle system
public class BattleSystem : MonoBehaviour {
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;

    private void Start() {
        SetupBattle();
    }

    public void SetupBattle() {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Bokemon);
        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Bokemon);
    }
}
