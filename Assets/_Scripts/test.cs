using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        HeadLook.OnEyeContactGlobal += (transformA, transformB) =>
        {
           // Debug.Log($"{transformA.name} made eye contact with {transformB.name}");
        };
    }

    private void OnDestroy()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
