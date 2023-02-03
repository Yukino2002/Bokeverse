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

    public class Player {
        public string name;
        public string type;
        public int hp;
        public int attack;
        public int defence;
        public int speed;
        public string imageCID;
    }

    public string json;
    public Player player;

    [SerializeField] string test123;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Image image;
    
    // function to access the list of bokemon in the party from other scripts
    public List<Bokemon> Bokemons => bokemons;

    // create the learnable moves in the game
    LearnableMove scratch = new LearnableMove();
    LearnableMove ember = new LearnableMove();
    LearnableMove tackle = new LearnableMove();
    LearnableMove waterBlaster = new LearnableMove();

    
    private void Start() {
        // create the learnable moves in the game
        ember = CreateMove("Ember", "A weak fire attack", BokemonType.Fire, 100, 100, 25, 7);
        scratch = CreateMove("Scratch", "A weak cutting attack", BokemonType.Normal, 40, 100, 35, 3);
        tackle = CreateMove("Tackle", "A weak physical hit attack", BokemonType.Normal, 60, 100, 30, 5);
        waterBlaster = CreateMove("Water Blaster", "A strong water attack", BokemonType.Water, 150, 100, 25, 7);

        bokemons = new List<Bokemon>();
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
        bokemonBase.Type1 = type;
        bokemonBase.Type2 = BokemonType.None;
        bokemonBase.UID = uid;
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
        bokemon.Level = 15 + bokemon.Experience / 100;

        return bokemon;
    }

    // function to fetch healthy bokemon
    public Bokemon GetHealthyBokemon() {
        return bokemons.Where(b => b.HP > 0).OrderBy(b => b.HP).FirstOrDefault();
    }

    // function to distribute experience to the bokemon in the party
    async public void PartyGainExperience(int experience) {
        Bokemon bokemon = bokemons[0];
        bokemon.Experience += experience;
        bokemon.Level = 15 + bokemon.Experience / 100;

        var contract = SDKManager.Instance.SDK.GetContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
        var result = await contract.Write("increaseExperience", bokemon.Base.UID, experience);
        Debug.Log(result);
    }

    // function to fetch the bokemon in the party from the contract
    public async void fetchBokemons() {
        // create a contract instance
        var contract = SDKManager.Instance.SDK.GetContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
        // get the player's wallet address
        string playerAddress = await SDKManager.Instance.SDK.wallet.GetAddress();
        
        // debug purposes to print the player's wallet address
        _title.text = playerAddress;
        
        // get the list of metadata and uid from the blockchain
        List<string> metadata = await contract.Read<List<string>>("getMetaDataBokemonPerUser", playerAddress);
        List<int> uid = await contract.Read<List<int>>("getBokemonPerUser", playerAddress);

        // string ipfs = "bafkreickp2dvdvz4rzd62hkzv2m2agi6tfsfhj2so5s3dpu5vjbr2cxswi";
        // StartCoroutine(LoadString("https://cloudflare-ipfs.com/ipfs/" + ipfs, 1));

        // loop through the list of metadata and uid parallelly
        // for (int i = 0; i < metadata.Count; i++) {
        //     string ipfs = metadata[i];
        //     int experience = await contract.Read<int>("experience", uid[i]);
        //     Debug.Log("IPFS: " + ipfs + " Experience: " + experience);
        //     StartCoroutine(LoadString("https://cloudflare-ipfs.com/ipfs/" + ipfs, experience));
        // }
    }

    IEnumerator LoadString(string url, int experience) {
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(www.error);
            } else {
                // Show results as text
                json = www.downloadHandler.text;
                _title.text = json;
                player = JsonUtility.FromJson<Player>(json);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                yield return StartCoroutine(GenerateBokemon("https://cloudflare-ipfs.com/ipfs/" + player.imageCID, player, experience));
            }
        }
    }
    
    IEnumerator GenerateBokemon(string url, Player bokemon, int experience) {
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

            Bokemon playerBokemon = CreateBokemon(bokemon.name, bokemon.name + " is a handsome like asim type bokemon", sprite, BokemonType.Fire, 1, bokemon.hp, bokemon.attack, bokemon.defence, bokemon.speed, experience, new List<LearnableMove> { ember, scratch, tackle });
            bokemons.Add(playerBokemon);

            playerBokemon.Init();
        }
    }
}