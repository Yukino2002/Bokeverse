using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Thirdweb;
using System.Linq;
using System.IO;

public class BokemonParty : MonoBehaviour {
    List<Bokemon> bokemons;

    [System.Serializable]
    public class Property
    {
        public string name;
        public string type;
        public int hp;
        public int attack;
        public int defense;
        public int speed;
    }

    [System.Serializable]
    public class Player {
        public string name;
        public string description;
        public string image;
        public Property[] properties;
    }

    public Player player;
    public string json;

    [SerializeField] GameObject transactionMessage;
    [SerializeField] PlayerController playerController;

    [SerializeField] string test123;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Image image;
    
    // function to access the list of bokemon in the party from other scripts
    public List<Bokemon> Bokemons => bokemons;

    // create the learnable moves in the game
    LearnableMove superPunch = new LearnableMove();
    LearnableMove bodySlam = new LearnableMove();
    LearnableMove fireCannon = new LearnableMove();
    LearnableMove lavaShoot = new LearnableMove();
    LearnableMove iceBlaster = new LearnableMove();
    LearnableMove snowStorm = new LearnableMove();
    LearnableMove earthBomb = new LearnableMove();
    LearnableMove earthQuake = new LearnableMove();
    LearnableMove steelBlade = new LearnableMove();
    LearnableMove steelDash = new LearnableMove();
    LearnableMove grassTornado = new LearnableMove();
    LearnableMove grassSlice = new LearnableMove();
    
    private void Start() {
        bokemons = new List<Bokemon>();

        superPunch = CreateMove("Super Punch", "A weak cutting attack", BokemonType.Normal, 60, 100, 35, 3);
        bodySlam = CreateMove("Body Slam", "A weak physical hit attack", BokemonType.Normal, 80, 100, 30, 5);
        fireCannon = CreateMove("Fire Cannon", "A fire attack", BokemonType.Fire, 140, 100, 35, 7);
        lavaShoot = CreateMove("Lava Shoot", "A fire attack", BokemonType.Fire, 120, 100, 30, 10);
        iceBlaster = CreateMove("Ice Blaster", "A ice attack", BokemonType.Ice, 110, 100, 25, 7);
        snowStorm = CreateMove("Snow Storm", "A ice attack", BokemonType.Ice, 140, 100, 30, 10);
        earthBomb = CreateMove("Earth Bomb", "A earth attack", BokemonType.Earth, 120, 100, 30, 7);
        earthQuake = CreateMove("Earth Quake", "A earth attack", BokemonType.Earth, 170, 100, 20, 10);
        steelBlade = CreateMove("Steel Blade", "A steel attack", BokemonType.Steel, 170, 100, 20, 7);
        steelDash = CreateMove("Steel Dash", "A steel attack", BokemonType.Steel, 140, 100, 25, 10);
        grassTornado = CreateMove("Grass Tornado", "A grass attack", BokemonType.Grass, 130, 100, 20, 7);
        grassSlice = CreateMove("Grass Slice", "A grass attack", BokemonType.Grass, 140, 100, 25, 10);
    }

    // function to create a learnable move object in the game
    private LearnableMove CreateMove(string name, string description, BokemonType type, int power, int accuracy, int pp, int level) {
        MoveBase move = ScriptableObject.CreateInstance<MoveBase>();
        move.Name = name;
        move.Description = description;
        move.Type = type;
        move.Power = power;
        move.Accuracy = accuracy;
        move.PP = pp;
        
        LearnableMove learnableMove = new LearnableMove();
        learnableMove.Base = move;
        learnableMove.Level = level;

        return learnableMove;
    }

    // function to create a bokemon object in the game from the data fetched from the blockchain
    private Bokemon CreateBokemon(string name, string description, Sprite sprite, BokemonType type, int uid, int hp, int attack, int defense, int speed, int experience, List<LearnableMove> learnableMoves) {
        BokemonBase bokemonBase = ScriptableObject.CreateInstance<BokemonBase>();
        bokemonBase.Name = name;
        bokemonBase.Description = description;
        bokemonBase.FrontSprite = sprite;
        bokemonBase.BackSprite = sprite;
        bokemonBase.Type = type;
        bokemonBase.MaxHP = hp;
        bokemonBase.Attack = attack;
        bokemonBase.Defense = defense;
        bokemonBase.SpecialAttack = attack + 20;
        bokemonBase.SpecialDefense = defense + 20;
        bokemonBase.Speed = speed;

        bokemonBase.LearnableMoves = new List<LearnableMove>();
        bokemonBase.LearnableMoves.AddRange(learnableMoves);

        Bokemon bokemon = new Bokemon();
        bokemon.Base = bokemonBase;
        bokemon.Experience = experience;
        bokemon.UID = uid;
        bokemon.Level = 7 + bokemon.Experience / 100;

        return bokemon;
    }

    // function to fetch healthy bokemon
    public Bokemon GetHealthyBokemon() {
        return bokemons.Where(b => b.HP > 0).OrderBy(b => b.HP).FirstOrDefault();
    }

    // function to heal the bokemons in the party
    public void HealBokemons() {
        foreach (Bokemon bokemon in bokemons) {
            bokemon.HP = bokemon.MaxHP;
        }
    }

    // function to distribute experience to the bokemon in the party
    async public void PartyGainExperience(int experience) {
        transactionMessage.SetActive(true);
        
        try {
            var ids = new int[bokemons.Count];
            var exp = new int[bokemons.Count];
            for (int i = 0; i < bokemons.Count; i++) {
                ids[i] = bokemons[i].UID;
                exp[i] = (int) experience * (1 - UnityEngine.Random.Range(0, 10) / 10);
            }

            var contract = SDKManager.Instance.SDK.GetContract("0xfbFaAB92b0444c36770190F22ea0C116B0Dea1a2");
            var result = await contract.Write("increaseExperienceBatch", ids, exp);

            // function increaseExperienceBatch(uint256[] memory _ids, uint256[] memory _experience)
            
            Debug.Log(result);

            if (result.isSuccessful()) {
                Debug.Log("Transaction successful");
                for (int i = 0; i < bokemons.Count; i++) {
                    bokemons[i].Experience += exp[i];
                    bokemons[i].Level = 7 + bokemons[i].Experience / 100;
                    bokemons[i].Init();
                }
            } else {
                Debug.Log("Transaction failed");
            }
        } catch (System.Exception e) {
            Debug.Log(e);
        }
        
        transactionMessage.SetActive(false);
    }

    // function to reward a starter bokemon to the player
    public async void GetStarterBokemon() {
        transactionMessage.SetActive(true);
        
        try {
            var contract = SDKManager.Instance.SDK.GetContract("0xfbFaAB92b0444c36770190F22ea0C116B0Dea1a2");
            string playerAddress = await SDKManager.Instance.SDK.wallet.GetAddress();
            var result = await contract.Write("mint", playerAddress, "ipfs://QmSgbfYSXEN1mZKwf4uWdUX1XgT7nyXB2KXBb6DxzB2jN2/0", 1);
            
            Debug.Log(result);

            if (result.isSuccessful()) {
                Debug.Log("Transaction successful");
                fetchBokemons();
            } else {
                playerController.StarterBokemon = true;
                Debug.Log("Transaction failed");
            }
        } catch (System.Exception e) {
            playerController.StarterBokemon = true;
            Debug.Log(e);
        }

        transactionMessage.SetActive(false);
    }

    public int GetHighestLevelBokemon() {
        int highestLevel = 0;
        foreach (Bokemon bokemon in bokemons) {
            if (bokemon.Level > highestLevel) {
                highestLevel = bokemon.Level;
            }
        }

        return highestLevel;
    }

    // function to fetch the bokemon in the party from the contract
    public async void fetchBokemons() {
        var contract = SDKManager.Instance.SDK.GetContract("0xfbFaAB92b0444c36770190F22ea0C116B0Dea1a2");
        string playerAddress = await SDKManager.Instance.SDK.wallet.GetAddress();
        
        // debug purposes to print the player's wallet address
        _title.text = playerAddress;
        
        // get the list of metadata and uid from the blockchain
        List<int> uid = await contract.Read<List<int>>("getBokemonPerUser", playerAddress);

        // string ipfs = "ipfs://QmT53i4kjSKGkNZgi8tprkBt7vk6PffuT1LKdPGXQud742/0";
        // StartCoroutine(LoadString("https://gateway.ipfscdn.io/ipfs/" + ipfs.Substring(7), 1, 1));

        for (int i = 0; i < uid.Count; i++) {
            string ipfs = await contract.Read<string>("getBokemonUri", uid[i]);
            int experience = await contract.Read<int>("experience", uid[i]);
            Debug.Log("IPFS: " + ipfs + " Experience: " + experience);
            bool create = true;
            for (int j = 0; j < bokemons.Count; j++) {
                if (bokemons[j].UID == uid[i]) {
                    create = false;
                    break;
                }
            }
            if (create) {
                StartCoroutine(LoadString("https://gateway.ipfscdn.io/ipfs/" + ipfs.Substring(7), experience, uid[i]));
            }
        }

        // sort the bokemons by uid
        bokemons.Sort((x, y) => x.UID.CompareTo(y.UID));
    }

    IEnumerator LoadString(string url, int experience, int uid) {
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(www.error);
            } else {
                // Show results as text
                json = www.downloadHandler.text;
                _title.text = json;
                player = JsonUtility.FromJson<Player>(json);
                Debug.Log(player.properties[0].type);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                yield return StartCoroutine(GenerateBokemon("https://gateway.ipfscdn.io/ipfs/" + player.image.Substring(7), player, experience, uid));
            }
        }
    }
    
    IEnumerator GenerateBokemon(string url, Player bokemon, int experience, int uid) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log(www.error);
        } else {
            // Get downloaded asset bundle
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;

            Debug.Log(experience);

            // list of learnable moves
            List <LearnableMove> learnableMoves = new List<LearnableMove> { superPunch, bodySlam };
            if (bokemon.properties[0].type == "fire") {
                learnableMoves.Add(fireCannon);
                learnableMoves.Add(lavaShoot);
            } else if (bokemon.properties[0].type == "ice") {
                learnableMoves.Add(iceBlaster);
                learnableMoves.Add(snowStorm);
            } else if (bokemon.properties[0].type == "grass") {
                learnableMoves.Add(grassTornado);
                learnableMoves.Add(grassSlice);
            } else if (bokemon.properties[0].type == "steel") {
                learnableMoves.Add(steelBlade);
                learnableMoves.Add(steelDash);
            } else if (bokemon.properties[0].type == "earth") {
                learnableMoves.Add(earthBomb);
                learnableMoves.Add(earthQuake);
            }

            Bokemon playerBokemon = CreateBokemon(
                bokemon.name, 
                bokemon.description, 
                sprite, 
                BokemonType.Fire, 
                uid, 
                bokemon.properties[0].hp, 
                bokemon.properties[0].attack, 
                bokemon.properties[0].defense, 
                bokemon.properties[0].speed, 
                experience,
                learnableMoves
            );

            bokemons.Add(playerBokemon);
            playerBokemon.Init();
        }
    }
}