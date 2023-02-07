using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Bokemon/Create new move")]
public class MoveBase : ScriptableObject {
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] BokemonType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public BokemonType Type { get => type; set => type = value; }
    public int Power { get => power; set => power = value; }
    public int Accuracy { get => accuracy; set => accuracy = value; }
    public int PP { get => pp; set => pp = value; }
    public bool isSpecial { get => type == BokemonType.Fire || type == BokemonType.Ice || type == BokemonType.Grass || type == BokemonType.Earth || type == BokemonType.Steel; }
}
