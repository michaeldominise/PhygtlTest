using System.Collections.Generic;
using UnityEngine;

public class GLTFSelection : MonoBehaviour
{
    public static GLTFSelection Instance { get; private set; }

    [SerializeField] GameObject container;
    [SerializeField] List<GLTFItemPreview> gltfItemPreview;

    private void Awake() => Instance = this;

    public void Init(List<GLTFMetadata> gLTFMetadataList)
    {
        var randomizedList = Randomize(gLTFMetadataList);
        for (var x = 0; x < gltfItemPreview.Count; x++)
            gltfItemPreview[x].Init(randomizedList[x], () => Show(false));
    }

    public List<T> Randomize<T>(List<T> list)
    {
        var newList = new List<T>();
        list.ForEach(x => newList.Insert(Random.Range(0, newList.Count), x));
        return newList;
    }

    public void Show(bool isShow) => container.SetActive(isShow);
}
