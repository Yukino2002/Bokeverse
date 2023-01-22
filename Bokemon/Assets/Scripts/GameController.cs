using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;

public enum GameState {
    FreeRoam,
    Battle
}

public class GameController : MonoBehaviour {
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    public Text walletInfotext;
    GameState state;

    private ThirdwebSDK sdk;

    private void Start() {
        sdk = new ThirdwebSDK("goerli");
        ConnectWallet(WalletProvider.MetaMask);

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
        // await EnsureCorrectWalletState();
        // reset and start a new battle
        battleSystem.StartBattle();
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

    private async void ConnectWallet(WalletProvider provider)
    {
        walletInfotext.text = "Connecting...";
        try
        {
            string address = await sdk.wallet.Connect(new WalletConnection()
            {
                provider = provider,
                chainId = 5 // Switch the wallet Goerli on connection
            });
            walletInfotext.text = "Connected as: " + address + " (" + provider + ")";
        }
        catch (System.Exception e)
        {
            walletInfotext.text = "Error (see console): " + e.Message;
        }
    }
}