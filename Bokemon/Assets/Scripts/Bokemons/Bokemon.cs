using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bokemon {
    BokemonBase _base;
    int level;
    public int HP { get; set; }

    // move list for the bokemon
    public List<Move> Moves { get; set; }

    // constructor
    public Bokemon(BokemonBase pBase, int pLevel) {
        _base = pBase;
        level = pLevel;
        HP = _base.MaxHP;

        // initialize the move list
        Moves = new List<Move>();
        foreach (var move in _base.LearnableMoves) {
            if (move.Level <= level) {
                Moves.Add(new Move(move.Base));
            }

            if (Moves.Count >= 4) {
                break;
            }
        }
    }

    // actual stats for the bokenmons depend on the base stats and the level
    public int Attack { get => Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    public int Defense { get => Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }
    public int SpecialAttack { get => Mathf.FloorToInt((_base.SpecialAttack * level) / 100f) + 5; }
    public int SpecialDefense { get => Mathf.FloorToInt((_base.SpecialDefense * level) / 100f) + 5; }
    public int Speed { get => Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    public int MaxHP { get => Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 20; }
}
