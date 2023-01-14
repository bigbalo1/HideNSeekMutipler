using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        InitializeCostume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitializeCostume()
    {
        int nb = Random.Range(0, 1000) % materials.Length;
        gameObject.GetComponent<Renderer>().material = materials[nb] as Material;
    }
}
