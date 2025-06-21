using System;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GLTFItemPreview : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI title;

    public GLTFMetadata gltfMetadata;
    private Action onSelect;

    private void Start() => button.onClick.AddListener(OnClick);

    public async void Init(GLTFMetadata gltfMetadata, Action onSelect)
    {
        this.gltfMetadata = gltfMetadata;
        this.onSelect = onSelect;
        title.text = gltfMetadata.Title;
        image.sprite = await DownloadTexture(gltfMetadata.ImageUrl);
    }

    async Task<Sprite> DownloadTexture(string url)
    {
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("Error downloading texture: " + request.error);
        }

        return null;
    }

    private async void OnClick()
    {
        onSelect?.Invoke();
        GLTFLoaderMessage.Instance.Show(true);
        await GLTFObjectLoader.Instance.Load(gltfMetadata.GltfUrl);
        GLTFLoaderMessage.Instance.Show(false);
    }

    void Reset() => button = GetComponent<Button>();
}
