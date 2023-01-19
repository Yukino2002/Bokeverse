using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bokemon {
    BokemonBase _base;
    int level;

    // constructor
    public Bokemon(BokemonBase pBase, int pLevel) {
        _base = pBase;
        level = pLevel;
    }

    // actual stats for the bokenmons depend on the base stats and the level
    public int Attack { get => Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    public int Defense { get => Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }
    public int SpecialAttack { get => Mathf.FloorToInt((_base.SpecialAttack * level) / 100f) + 5; }
    public int SpecialDefense { get => Mathf.FloorToInt((_base.SpecialDefense * level) / 100f) + 5; }
    public int Speed { get => Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    public int MaxHP { get => Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 20; }
}
