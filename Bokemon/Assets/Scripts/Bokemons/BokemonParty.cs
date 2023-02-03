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
    
    public List<Bokemon> Bokemons => bokemons;
    LearnableMove scratch = new LearnableMove();
    LearnableMove ember = new LearnableMove();
    LearnableMove tackle = new LearnableMove();
    LearnableMove waterBlaster = new LearnableMove();
    
    private void Start() {
        ember = CreateMove("Ember", "A weak fire attack", BokemonType.Fire, 100, 100, 25, 7);
        scratch = CreateMove("Scratch", "A weak cutting attack", BokemonType.Normal, 40, 100, 35, 3);
        tackle = CreateMove("Tackle", "A weak physical hit attack", BokemonType.Normal, 60, 100, 30, 5);
        waterBlaster = CreateMove("Water Blaster", "A strong water attack", BokemonType.Water, 150, 100, 25, 7);

        bokemons = new List<Bokemon>();
    }

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

    private Bokemon CreateBokemon(string name, string description, Sprite sprite, BokemonType type, int uid, int hp, int attack, int defense, int speed, int exp, List<LearnableMove> learnableMoves) {
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
        bokemon.Level = 10 + exp / 100;

        return bokemon;
    }

    public Bokemon GetHealthyBokemon() {
        return bokemons.Where(b => b.HP > 0).OrderBy(b => b.HP).FirstOrDefault();
    }

    public async void fetchBokemons() {
        var contract = SDKManager.Instance.SDK.GetContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
        string addressArg = await SDKManager.Instance.SDK.wallet.GetAddress();
        _title.text = addressArg;
        List<string> contractRaw = await contract.Read<List<string>>("getMetaDataBokemonPerUser", addressArg);
        string ipfs = contractRaw[0];
        // string ipfs = "bafkreickp2dvdvz4rzd62hkzv2m2agi6tfsfhj2so5s3dpu5vjbr2cxswi";
        StartCoroutine(LoadString("https://cloudflare-ipfs.com/ipfs/" + ipfs));
    }

    IEnumerator LoadString(string url) {
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(www.error);
            } else {
                // Show results as text
                json = www.downloadHandler.text;
                _title.text = json;
                player = JsonUtility.FromJson<Player>(json);
                Debug.Log(player.name);
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                yield return StartCoroutine(GenerateBokemon("https://cloudflare-ipfs.com/ipfs/" + player.imageCID, player));
            }
        }
    }
    
    IEnumerator GenerateBokemon(string url, Player bokemon) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log(www.error);
        } else {
            // Get downloaded asset bundle
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;

            Debug.Log("Trying to fetch contract");
            var contract = SDKManager.Instance.SDK.GetContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
            Debug.Log("Contract fetched");
            Debug.Log("Trying to fetch experience");

            int experience;
            var task = Task.Run(async () => (int) await contract.Read<int>("experience", 1));
            yield return new WaitUntil(() => task.IsCompleted);
            experience = task.Result;
            Debug.Log("Experience fetched");
            Debug.Log(experience);

            Bokemon bokemon1 = CreateBokemon(bokemon.name, bokemon.name + " is a handsome like asim type bokemon", sprite, BokemonType.Fire, 1, bokemon.hp, bokemon.attack, bokemon.defence, bokemon.speed, experience, new List<LearnableMove> { ember, scratch, tackle });
            bokemons.Add(bokemon1);

            _title.text = "No_Work";
            
            foreach (var boke in bokemons) {
                boke.Init();
            
            }
            _title.text = "Work";
            
        }
    }
}
