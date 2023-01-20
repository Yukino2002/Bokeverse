using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour {
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    // refernce to the hp bar script
    [SerializeField] HPBar hpBar;

    Bokemon _bokemon;

    // set the name, level, and pass normalized hp of the bokemon
    public void SetData(Bokemon bokemon) {
        _bokemon = bokemon;

        nameText.text = "   " + bokemon.Base.Name;
        levelText.text = "Lvl " + bokemon.Level + "   ";
        hpBar.SetHP((float) bokemon.HP / bokemon.MaxHP);
    }

    public IEnumerator UpdateHP() {
        yield return hpBar.SetHPSmooth((float) _bokemon.HP / _bokemon.MaxHP);
    }
}
