using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyScreen : MonoBehaviour {
    [SerializeField] Text messageText;
    
    PartyMemberUI[] memberSlots;

    public void Init() {
        memberSlots = GetComponentsInChildren<PartyMemberUI>();
    }

    public void SetPartyData(List<Bokemon> bokemons) {
        for (int i = 0; i < memberSlots.Length; i++) {
            if (i < bokemons.Count) {
                memberSlots[i].SetData(bokemons[i]);
            } else {
                memberSlots[i].gameObject.SetActive(false);
            }
        }

        messageText.text = "Choose a Bokemon";
    }
}