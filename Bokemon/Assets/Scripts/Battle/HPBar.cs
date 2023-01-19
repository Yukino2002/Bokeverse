using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {
    // reference to the health bar image in the game
    [SerializeField] GameObject health;

    // normalize it as per our bokemon
    public void SetHP(float hpNormalized) {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }
}
