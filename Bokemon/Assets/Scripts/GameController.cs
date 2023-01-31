using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Thirdweb;

public enum GameState {
    FreeRoam,
    Battle
}

public class GameController : MonoBehaviour {
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] Camera minimapCamera;
    [SerializeField] GameObject menuSystem;

    
    GameState state;

    private ThirdwebSDK sdk;
    public Text walletInfotext;

    public void Start() {
        // set the game state to free roam
        state = GameState.FreeRoam;
        // subscribe to the OnEncounter event
        playerController.OnEncounter += StartBattle;
        // subscribe to the OnBattleOver event
        battleSystem.OnBattleOver += EndBattle;

        // // Instantise the ThirdwebSDK
        // sdk = new ThirdwebSDK("optimism-goerli");
    }

    private void StartBattle() {
        // set the game state to battle mode
        state = GameState.Battle;
        // as the battle system is initially disabled, enable it
        battleSystem.gameObject.SetActive(true);
        // disable our main camera, so that the battle camera can be used
        worldCamera.gameObject.SetActive(false);

        // reset and start a new battle
        var playerParty = playerController.GetComponent<BokemonParty>();
        var wildBokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildBokemon();
        battleSystem.StartBattle(playerParty, wildBokemon);
    }

    void EndBattle(bool won) {
        // set the game state to free roam
        state = GameState.FreeRoam;
        // disable the battle system
        battleSystem.gameObject.SetActive(false);
        // enable the main camera
        worldCamera.gameObject.SetActive(true);
    }

    private void Update() {
        if (state == GameState.FreeRoam) {
            playerController.HandleUpdate();
        } else if (state == GameState.Battle) {
            battleSystem.HandleUpdate();
        }
    }

    public async void ConnectWallet() {
        sdk = new ThirdwebSDK("goerli");
        walletInfotext.text = "Connecting...";
        try {
            string address = await sdk.wallet.Connect(new WalletConnection() {
                provider = WalletProvider.MetaMask,
                chainId = 5
            });
            walletInfotext.text = address;
        }
        catch (System.Exception e) {
            walletInfotext.text = "Error (see console): " + e.Message;
        }

        menuSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        minimapCamera.gameObject.SetActive(true);
    }
}