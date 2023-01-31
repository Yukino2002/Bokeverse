using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour {
    [SerializeField] List<Bokemon> wildBokemons;

    // get a random bokemon from the list of wild bokemons
    public Bokemon GetRandomWildBokemon() {
        int randomIndex = Random.Range(0, wildBokemons.Count);
        var wildBokemon = wildBokemons[randomIndex];
        wildBokemon.Init();
        return wildBokemon;
    }
}