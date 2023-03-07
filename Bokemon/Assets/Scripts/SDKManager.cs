using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;

public class SDKManager : MonoBehaviour
{
    public static SDKManager Instance;
    public ThirdwebSDK SDK;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SDK = new ThirdwebSDK("https://rpcapi.fantom.network/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
