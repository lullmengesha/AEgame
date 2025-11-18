using UnityEngine;
using System.Collections.Generic;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [System.Serializable]
    public class VFX
    {
        public string name;         // name you will call
        public GameObject prefab;   // particle prefab
        public float destroyAfter = 2f;
    }

    public List<VFX> vfxList = new List<VFX>();
    private Dictionary<string, VFX> vfxDict;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        vfxDict = new Dictionary<string, VFX>();

        foreach (var v in vfxList)
        {
            vfxDict.Add(v.name, v);
        }
    }

    // Play at position
    public GameObject Play(string vfxName, Vector3 position)
    {
        if (!vfxDict.TryGetValue(vfxName, out VFX v))
        {
            Debug.LogWarning($"VFXManager: VFX '{vfxName}' not found.");
            return null;
        }

        GameObject obj = Instantiate(v.prefab, position, Quaternion.identity);
        Destroy(obj, v.destroyAfter);
        return obj;
    }

    // Play at position + rotation (optional)
    public GameObject Play(string vfxName, Vector3 position, Quaternion rotation)
    {
        if (!vfxDict.TryGetValue(vfxName, out VFX v))
        {
            Debug.LogWarning($"VFXManager: VFX '{vfxName}' not found.");
            return null;
        }

        GameObject obj = Instantiate(v.prefab, position, rotation);
        Destroy(obj, v.destroyAfter);
        return obj;
    }
}
