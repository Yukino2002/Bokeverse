using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// scriptable objects are data containers for the game
// helps to create a new scriptable object in the project window
[CreateAssetMenu(fileName = "Bokemon", menuName = "Bokemon/Create new bokemon")]
public class BokemonBase : ScriptableObject {
    // use this variable outside this class, but bad practice to make it public
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] BokemonType type;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int specialAttack;
    [SerializeField] int specialDefense;
    [SerializeField] int speed;

    // list of learnable moves by bokemon and their level
    [SerializeField] List<LearnableMove> learnableMoves;

    // getter properties in C#, use capital letter for the first letter to get the value of the variable
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite FrontSprite { get => frontSprite; set => frontSprite = value; }
    public Sprite BackSprite { get => backSprite; set => backSprite = value; }
    public BokemonType Type { get => type; set => type = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int SpecialAttack { get => specialAttack; set => specialAttack = value; }
    public int SpecialDefense { get => specialDefense; set => specialDefense = value; }
    public int Speed { get => speed; set => speed = value; }
    public List<LearnableMove> LearnableMoves { get => learnableMoves; set => learnableMoves = value; }
}

// learnable move class for a bokemon, display in inspector
[System.Serializable]
public class LearnableMove {
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base { get => moveBase; set => moveBase = value; }
    public int Level { get => level; set => level = value; }
}

public enum BokemonType {
    None,
    Normal,
    Fire,
    Earth,
    Ice,
    Steel,
    Grass
}

// chart for type effectiveness, 2f super effective, 0f not effective, 1f normal
public class TypeChart {
    // static to use it directly from the class without creating an instance
    static float[][] chart = {
        /*                      NOR FIR ERT ICE STL GRS */
        /* NOR */ new float[] { 1f, 1f, 1f, 1f, 1f, 1f },
        /* FIR */ new float[] { 1f, 1f, 0.5f, 2f, 1f, 2f },
        /* ERT */ new float[] { 1f, 2f, 1f, 1f, 1f, 0.5f },
        /* ICE */ new float[] { 1f, 0.5f, 1f, 1f, 2f, 1f },
        /* STL */ new float[] { 1f, 2f, 1f, 0.5f, 1f, 1f },
        /* GRS */ new float[] { 1f, 0.5f, 0.5f, 1f, 1f, 1f }
    };

    // get the effectiveness of a move against a bokemon
    public static float GetEffectiveness(BokemonType attackType, BokemonType defenseType) {
        if (attackType == BokemonType.None || defenseType == BokemonType.None) {
            return 1f;
        }

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}