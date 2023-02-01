using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyScreen : MonoBehaviour {
    [SerializeField] Text messageText;
    
    PartyMemberUI[] memberSlots;
    List<Bokemon> bokemons;

    public void Init() {
        memberSlots = GetComponentsInChildren<PartyMemberUI>();
    }

    public void SetPartyData(List<Bokemon> bokemons) {
        this.bokemons = bokemons;

        for (int i = 0; i < memberSlots.Length; i++) {
            if (i < bokemons.Count) {
                memberSlots[i].SetData(bokemons[i]);
            } else {
                memberSlots[i].gameObject.SetActive(false);
            }
        }

        messageText.text = "Choose a Bokemon";
    }

    public void UpdateMemberSelection(int selectedMember) {
        for (int i = 0; i < bokemons.Count; i++) {
            memberSlots[i].SetSelected(i == selectedMember);
        }
    }

    public void SetMessageText(string message) {
        messageText.text = message;
    }
}