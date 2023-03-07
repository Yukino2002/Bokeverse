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
    [SerializeField] BokemonParty bokemonParty;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] Camera minimapCamera;
    [SerializeField] GameObject menuSystem;
    [SerializeField] GameObject GameMusic;
    [SerializeField] GameObject BattleMusic;
    [SerializeField] GameObject bokemonMaster;

    
    GameState state;

    public Text walletInfotext;
    string address;
    
    public void Start() {
        // set the game state to free roam
        state = GameState.FreeRoam;
        // set game music to active
        GameMusic.SetActive(true);
        // subscribe to the OnEncounter event
        playerController.OnEncounter += StartBattle;
        // subscribe to the OnBattleOver event
        battleSystem.OnBattleOver += EndBattle;
    }

    private void StartBattle() {
        // set the game state to battle mode
        state = GameState.Battle;
        // set game music to inactive
        GameMusic.SetActive(false);
        // set battle music to active
        BattleMusic.SetActive(true);
        // as the battle system is initially disabled, enable it
        battleSystem.gameObject.SetActive(true);
        // disable our main and minimap camera, so that the battle camera can be used
        worldCamera.gameObject.SetActive(false);
        minimapCamera.gameObject.SetActive(false);

        // reset and start a new battle
        var playerParty = playerController.GetComponent<BokemonParty>();
        var wildBokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildBokemon();

        // initialize the wild bokemon with random level
        wildBokemon.Level = UnityEngine.Random.Range(1, 20);
        wildBokemon.Init();
        battleSystem.StartBattle(playerParty, wildBokemon);
    }

    // function to end the battle, and exit back to free roam
    void EndBattle(bool won) {
        // set the game state to free roam
        state = GameState.FreeRoam;
        // set battle music to inactive
        BattleMusic.SetActive(false);
        // set game music to active
        GameMusic.SetActive(true);
        // disable the battle system
        battleSystem.gameObject.SetActive(false);
        // enable the main camera and minimap
        worldCamera.gameObject.SetActive(true);
        minimapCamera.gameObject.SetActive(true);

        if (won) {
            bokemonParty.PartyGainExperience(UnityEngine.Random.Range(1, 50) * 10);
        }
    }

    // Update is called once per frame
    private void Update() {
        if (state == GameState.FreeRoam) {
            if(bokemonParty.GetHighestLevelBokemon() >= 25) {
                bokemonMaster.SetActive(true);
            }
            playerController.HandleUpdate();
        } else if (state == GameState.Battle) {
            bokemonMaster.SetActive(false);
            battleSystem.HandleUpdate();
        }
    }

    // common function to connect to a wallet
    public async void ConnectWallet(WalletProvider provider) {
        try {
            address = await SDKManager.Instance.SDK.wallet.Connect(new WalletConnection() {
                provider = provider,
                chainId = 250
            });
            walletInfotext.text = address;
            menuSystem.gameObject.SetActive(false);
            worldCamera.gameObject.SetActive(true);
            minimapCamera.gameObject.SetActive(true);
            
        }
        catch (System.Exception e) {
            walletInfotext.text = "Error (see console): " + e.Message;
        }
        bokemonParty.fetchBokemons();
    }

    // function to connect to Metamask Wallet
    public async void MetamaskLogin() {
        ConnectWallet(WalletProvider.MetaMask);
    }

    // function to connect to Coinbase Wallet
    public async void CoinbaseWalletLogin() {
        ConnectWallet(WalletProvider.CoinbaseWallet);
    }

    // function to connect to other popular wallets
    public async void WalletConnectLogin() {
        ConnectWallet(WalletProvider.WalletConnect);
    }
}