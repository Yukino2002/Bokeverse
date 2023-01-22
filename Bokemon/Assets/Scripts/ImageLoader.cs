using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private Image image;
    private string url = "https://chargeswap.infura-ipfs.io/ipfs/QmaW6tLeiueN78BsVw2VycaaMWRVkzZRnjKWx4Wf8tC84b/2178.png";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadImage(url));
    }

    IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get downloaded asset bundle
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;
        }
    }
}
