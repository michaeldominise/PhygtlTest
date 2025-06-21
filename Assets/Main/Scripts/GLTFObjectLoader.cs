using System.Threading.Tasks;
using UnityEngine;
using UnityGLTF;

public class GLTFObjectLoader : MonoBehaviour
{
    public static GLTFObjectLoader Instance { get; private set; }

	[SerializeField] GLTFComponent gltfComponent;

	private void Awake() => Instance = this;
	private void Start() => GLTFSelection.Instance.Show(true);

	public async Task Load(string uri)
	{
		gltfComponent.GLTFUri = uri;
		await gltfComponent.Load();
	}

	public void Unload()
	{
		Destroy(gameObject);
		Resources.UnloadUnusedAssets();
	}

}
