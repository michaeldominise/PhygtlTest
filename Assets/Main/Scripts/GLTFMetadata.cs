using UnityEngine;

[System.Serializable]
public class GLTFMetadata
{
    [SerializeField, TextArea] string imageUrl;
    [SerializeField, TextArea] string gltfUrl;

    public string Title => imageUrl.Replace("https://github.com/KhronosGroup/glTF-Sample-Assets/blob/main/Models/", "").Split('/')[0];
    public string ImageUrl => $"{imageUrl}?raw=true";
    public string GltfUrl => $"{gltfUrl}?raw=true";
}
