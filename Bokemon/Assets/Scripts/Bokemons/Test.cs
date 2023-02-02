using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : ScriptableObject {
    // use this variable outside this class, but bad practice to make it public
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string description;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
}