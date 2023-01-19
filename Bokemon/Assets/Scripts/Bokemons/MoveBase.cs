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

    public string Name { get => name; }
    public string Description { get => description; }
    public BokemonType Type { get => type; }
    public int Power { get => power; }
    public int Accuracy { get => accuracy; }
    public int PP { get => pp; }
}
