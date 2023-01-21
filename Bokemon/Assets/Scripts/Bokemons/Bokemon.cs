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

    public DamageDetails TakeDamage(Move move, Bokemon attacker) {
        float critical = 1f;
        if (Random.value * 100f <= 6.25f) {
            critical = 2f;
        }

        // calculate the damage
        float type = TypeChart.GetEffectiveness(move.Base.Type, Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, Base.Type2);
        
        var damageDetails = new DamageDetails() {
            Fainted = false,
            Critical = critical,
            TypeEffectiveness = type
        };
        
        float modifiers = Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float b = a * move.Base.Power * (attacker.Attack / (float) Defense) + 2;
        float damage = Mathf.FloorToInt(b * modifiers);

        // apply the damage
        HP -= Mathf.FloorToInt(damage);

        // return true if the bokemon fainted
        if (HP <= 0) {
            HP = 0;
            damageDetails.Fainted = true;
        }
        
        return damageDetails;
    }

    public Move GetRandomMove() {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DamageDetails {
    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }
}