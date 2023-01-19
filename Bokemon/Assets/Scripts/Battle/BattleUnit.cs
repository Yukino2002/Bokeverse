using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour {
    [SerializeField] BokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Bokemon Bokemon { get; set; }

    public void Setup() {
        Bokemon = new Bokemon(_base, level);
        if (isPlayerUnit) {
            GetComponent<Image>().sprite = Bokemon.Base.BackSprite;
        } else {
            GetComponent<Image>().sprite = Bokemon.Base.FrontSprite;
        }
    }
}