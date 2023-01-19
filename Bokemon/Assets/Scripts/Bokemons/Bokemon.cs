using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bokemon {
    public BokemonBase Base { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }

    // move list for the bokemon
    public List<Move> Moves { get; set; }

    // constructor
    public Bokemon(BokemonBase pBase, int pLevel) {
        Base = pBase;
        Level = pLevel;
        HP = MaxHP;

        // initialize the move list
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves) {
            if (move.Level <= Level) {
                Moves.Add(new Move(move.Base));
            }

            if (Moves.Count >= 4) {
                break;
            }
        }
    }

    // actual stats for the bokenmons depend on the base stats and the level
    public int Attack { get => Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    public int Defense { get => Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    public int SpecialAttack { get => Mathf.FloorToInt((Base.SpecialAttack * Level) / 100f) + 5; }
    public int SpecialDefense { get => Mathf.FloorToInt((Base.SpecialDefense * Level) / 100f) + 5; }
    public int Speed { get => Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    public int MaxHP { get => Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 20; }
}
