using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bokemon {
    // need private variables to store the base and level of the bokemon
    [SerializeField] BokemonBase _base;
    [SerializeField] int _level;
    [SerializeField] int _experience;

    public BokemonBase Base { get => _base; set => _base = value; }
    public int Level { get => _level; set => _level = value; }
    public int Experience { get => _experience; set => _experience = value; }
    public int HP { get; set; }

    // move list for the bokemon
    public List<Move> Moves { get; set; }

    // no requirement of a constructor, because we initialize the bokemon from the inspector
    public void Init() {
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

        float attack = move.Base.isSpecial ? attacker.SpecialAttack : attacker.Attack;
        float defense = move.Base.isSpecial ? SpecialDefense : Defense;
        
        float modifiers = Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float b = a * move.Base.Power * (attack / (float) defense) + 2;
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