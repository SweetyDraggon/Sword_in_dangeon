using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Localization3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro text = GetComponent<TextMeshPro>();
        if (text != null)
        {
            Localisation.SetStringTo3DText(text);
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
