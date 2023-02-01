using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour {
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    [SerializeField] Color highlightedColor;

    Bokemon _bokemon;

    public void SetData(Bokemon bokemon) {
        _bokemon = bokemon;

        nameText.text = bokemon.Base.Name;
        levelText.text = "Lvl " + bokemon.Level;
        hpBar.SetHP((float)bokemon.HP / bokemon.MaxHP);
    }

    public void SetSelected(bool selected) {
       nameText.color = selected ? highlightedColor : Color.black;
    }
}