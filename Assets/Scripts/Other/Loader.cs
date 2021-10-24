using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Mst.Test
{
public class Loader : MonoBehaviour
{
    private readonly string bundleURL1 = "https://drive.google.com/uc?export=download&id=1jLpmarHEyED5YzRmfLghjm3mNml83B0i";
    private readonly string bundleURL2 = "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/_HltG_9jaWk4Xg";
    private string serverURL;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ServerNames serverNames;

    private enum ServerNames
    {
        Google = 1,
        Yandex = 2
    }

    private void Start() 
    {
        SwitchNames();
    }

    private void SwitchNames()
    {
        switch (serverNames)
        {
            case ServerNames.Google:
            serverURL = bundleURL1;
            break;
            case ServerNames.Yandex:
            serverURL = bundleURL2;
            break;

            default:
            serverURL = bundleURL1;
            break;
        }
    }

    public void DownloadOnClick() //BTN
    {
        StartCoroutine(DownloadAndChache());
        gameObject.GetComponent<Button>().interactable = false;
    }

    IEnumerator DownloadAndChache()
    {
        Debug.Log("Sending request...");
        UnityWebRequest www1 = UnityWebRequestAssetBundle.GetAssetBundle(serverURL); //bndl version+
        yield return www1.SendWebRequest();

        if (www1.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www1.error + " :ERROR");
            gameObject.GetComponent<Button>().interactable = true;
        } else
        {
            AssetBundle bundle1 = DownloadHandlerAssetBundle.GetContent(www1);
            Debug.Log($"FOUND: {bundle1} !!!");

            if(bundle1 != null)
            {
                var load1 =bundle1.LoadAssetAsync<Sprite>("purgen_spr");
                var load2 =bundle1.LoadAssetAsync<AudioClip>("pp_song");
                yield return load1;
                yield return load2;

                spriteRenderer.sprite = load1.asset as Sprite;
                audioSource.clip = load2.asset as AudioClip;
                audioSource.Play();
                Debug.Log("Finish");
                
            } else 
            {
                Debug.LogWarning("Not a valid asset bundle");
            }

            bundle1.Unload(false);
        }
    }

}
}
