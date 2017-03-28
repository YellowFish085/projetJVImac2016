using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistTextureInitialization : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var model = transform.FindChild("Cylinder_000_Cylinder_001");
        var material = model.GetComponent<SkinnedMeshRenderer>().material;

        Texture nTex = Resources.Load("zombieTextures/scientist") as Texture;

        material.SetTexture("_MainTex", nTex);
        material.SetTexture("_BumpMap", null);
        transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }
}
