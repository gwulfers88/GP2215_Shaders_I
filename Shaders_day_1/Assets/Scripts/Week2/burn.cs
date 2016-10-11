using UnityEngine;
using System.Collections;

public class burn : MonoBehaviour
{
    [SerializeField]
    float rate = 0.05f;
    Material burnMaterial;
    float Threshold1;
    float Threshold2;

	// Use this for initialization
	void Start ()
    {
        burnMaterial = GetComponent<Renderer>().material;
        if(burnMaterial.shader.name != "Custom/burn")
        {
            Destroy(this);
        }

        Threshold1 = burnMaterial.GetFloat("_Thresh1");
        Threshold2 = burnMaterial.GetFloat("_Thresh2");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Threshold2 += (Time.deltaTime * rate);
        Threshold1 += (Time.deltaTime * rate) * 0.5f;

        Threshold1 = Mathf.Clamp01(Threshold1);
        Threshold2 = Mathf.Clamp01(Threshold2);

        burnMaterial.SetFloat("_Thresh1", Threshold1);
        burnMaterial.SetFloat("_Thresh2", Threshold2);
    }
}
