using UnityEngine;

public class GLTFLoaderMessage : MonoBehaviour
{
    public static GLTFLoaderMessage Instance { get; private set; }

    [SerializeField] GameObject container;

    private void Awake() => Instance = this;

    public void Show(bool isShow) => container.SetActive(isShow);
}
