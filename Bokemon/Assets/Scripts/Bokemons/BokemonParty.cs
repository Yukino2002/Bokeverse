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

    public class Player
    {
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
    private void Start() {
        // MyScriptableObject someInstance = ScriptableObject.CreateInstance("MyScriptableObject") as MyScriptableObject;
        // someInstance.init (5, "Gorlock", 15, owningMap, someOtherParameter);

        // StartPokemonImport();
        // initialize the bokemons

    }

    public Bokemon GetHealthyBokemon() {
        return bokemons.Where(b => b.HP > 0).OrderBy(b => b.HP).FirstOrDefault();
    }

    public async void StartPokemonImport()
    {
        var contract = SDKManager.Instance.SDK.GetContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
        string addressArg = await SDKManager.Instance.SDK.wallet.GetAddress();
        _title.text = addressArg;
        List<string> contractRaw = await contract.Read<List<string>>("getMetaDataBokemonPerUser", addressArg);
        string ipfs = contractRaw[0];
        // string ipfs = "bafkreickp2dvdvz4rzd62hkzv2m2agi6tfsfhj2so5s3dpu5vjbr2cxswi";
        StartCoroutine(LoadString("https://cloudflare-ipfs.com/ipfs/"+ipfs));
    }

    IEnumerator LoadString(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                json = www.downloadHandler.text;
                _title.text = json;
                player = JsonUtility.FromJson<Player>(json);
                Debug.Log(player.name);
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                StartCoroutine(GenerateBokemon("https://cloudflare-ipfs.com/ipfs/"+player.imageCID, player));

            }
        }
    }
    
    IEnumerator GenerateBokemon(string url, Player bokemon)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get downloaded asset bundle
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;
            // Don't Change Start
            MoveBase emberBase = ScriptableObject.CreateInstance("MoveBase") as MoveBase;
            emberBase.Name = "Ember";
            emberBase.Description = "Mild fire move";
            emberBase.Type = BokemonType.Fire;
            emberBase.Power = 40;
            emberBase.Accuracy = 100;
            emberBase.PP = 25;

            Debug.Log(emberBase.Name);

            LearnableMove ember = new LearnableMove();
            ember.Base = emberBase;
            ember.Level = 5;

            Debug.Log(ember.Base.Name);
            // Dont Change End
            BokemonBase charaBase = ScriptableObject.CreateInstance("BokemonBase") as BokemonBase;
            charaBase.Name = bokemon.name;
            charaBase.Description = bokemon.name + " is a handsome like asim type bokemon";
            charaBase.FrontSprite = sprite;
            charaBase.BackSprite = sprite;
            charaBase.Type1 = BokemonType.Fire; 
            charaBase.Type2 = BokemonType.None;
            charaBase.MaxHP = bokemon.hp;
            charaBase.Attack = bokemon.attack;
            charaBase.Defense = bokemon.defence;
            charaBase.SpecialAttack = bokemon.attack;
            charaBase.SpecialDefense = bokemon.defence;
            charaBase.Speed = bokemon.speed;
            charaBase.LearnableMoves = new List<LearnableMove>();
            charaBase.LearnableMoves.Add(ember);

            Debug.Log(charaBase.Name);
            Debug.Log(charaBase.LearnableMoves[0].Base.Name);

            Bokemon chara = new Bokemon();
            chara.Base = charaBase;
            chara.Level = 5;

            Debug.Log(chara.Base.Name);
            Debug.Log(chara.Base.LearnableMoves[0].Base.Name);

            bokemons = new List<Bokemon>();
            bokemons.Add(chara);

            Debug.Log(bokemons[0].Base.Name);

            _title.text = "Work";
            
            foreach (var boke in bokemons) {
                boke.Init();
            
            }
            _title.text = "No_Work";
        }
    }
}
