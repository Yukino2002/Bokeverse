using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BokemonParty : MonoBehaviour {
    [SerializeField] List<Bokemon> bokemons;    

    private void Start() {
        // initialize the bokemons
        foreach (var bokemon in bokemons) {
            bokemon.Init();
        }
    }

    public Bokemon GetHealthyBokemon() {
        return bokemons.Where(b => b.HP > 0).OrderBy(b => b.HP).FirstOrDefault();
    }
}
