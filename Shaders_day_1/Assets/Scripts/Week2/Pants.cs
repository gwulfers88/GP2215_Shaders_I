using UnityEngine;
using System.Collections;

public class Pants : MonoBehaviour
{
    [SerializeField]
    Material material;
    [SerializeField]
    float rate = 0.5f;
    float Saturation;

    void Start()
    {

        material = GetComponent<Renderer>().material;

        if(material.shader.name != "Custom/Pants")
        {
            Destroy(this);
        }

        //Saturation = material.GetFloat("_Saturate");
    }

    void Update()
    {
        Saturation -= Time.deltaTime * rate;
        if (Saturation < 0.0f)
        {
            Saturation = 0.0f;
        }

        //material.SetFloat("_Saturate", Saturation);
    }
}
