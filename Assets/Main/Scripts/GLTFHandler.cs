using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLTFHandler : MonoBehaviour
{
    public static GLTFHandler Instance { get; private set; }

    [SerializeField] List<GLTFMetadata> gltfMetadataList;

    private void Awake() => Instance = this;

    IEnumerator Start()
    {
        yield return null;
        yield return null;
        Init();
    }

    public void Init() => GLTFSelection.Instance.Init(gltfMetadataList);
}
