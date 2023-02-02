using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BokemonParty : MonoBehaviour {
    List<Bokemon> bokemons;

    public List<Bokemon> Bokemons => bokemons;

    private void Start() {
        // MyScriptableObject someInstance = ScriptableObject.CreateInstance("MyScriptableObject") as MyScriptableObject;
        // someInstance.init (5, "Gorlock", 15, owningMap, someOtherParameter);

        Test test = ScriptableObject.CreateInstance("Test") as Test;
        test.Name = "test";
        test.Description = "test description";

        Debug.Log(test.Name);

        MoveBase emberBase = ScriptableObject.CreateInstance("MoveBase") as MoveBase;
        emberBase.Name = "Ember";
        emberBase.Description = "Mild fire move";
        emberBase.Type = BokemonType.Fire;
        emberBase.Power = 40;
        emberBase.Accuracy = 100;
        emberBase.PP = 25;

        Debug.Log(emberBase.Name);

        LearnableMove ember = new LearnableMove();
        ember.Base = emberBase;
        ember.Level = 5;

        Debug.Log(ember.Base.Name);

        BokemonBase charaBase = ScriptableObject.CreateInstance("BokemonBase") as BokemonBase;
        charaBase.Name = "Chara";
        charaBase.Description = "A fire bokemon";
        charaBase.FrontSprite = Resources.Load<Sprite>("chara-front");
        charaBase.BackSprite = Resources.Load<Sprite>("chara-back");
        charaBase.Type1 = BokemonType.Fire; 
        charaBase.Type2 = BokemonType.None;
        charaBase.MaxHP = 39;
        charaBase.Attack = 52;
        charaBase.Defense = 43;
        charaBase.SpecialAttack = 60;
        charaBase.SpecialDefense = 50;
        charaBase.Speed = 65;
        charaBase.LearnableMoves = new List<LearnableMove>();
        charaBase.LearnableMoves.Add(ember);

        Debug.Log(charaBase.Name);
        Debug.Log(charaBase.LearnableMoves[0].Base.Name);

        Bokemon chara = new Bokemon();
        chara.Base = charaBase;
        chara.Level = 5;

        Debug.Log(chara.Base.Name);
        Debug.Log(chara.Base.LearnableMoves[0].Base.Name);

        bokemons = new List<Bokemon>();
        bokemons.Add(chara);

        Debug.Log(bokemons[0].Base.Name);

        // initialize the bokemons
        foreach (var bokemon in bokemons) {
            bokemon.Init();
        }
    }

    public Bokemon GetHealthyBokemon() {
        return bokemons.Where(b => b.HP > 0).OrderBy(b => b.HP).FirstOrDefault();
    }
}
