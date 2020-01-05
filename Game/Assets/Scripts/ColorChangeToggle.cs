using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeToggle : MonoBehaviour
{
    public Material mat;
    public Toggle toggle;

    public Color primary = Color.white;
    public Color secondary = Color.blue;

    public void Update()
    {
        if (toggle.isOn)
        {
            mat.color = primary;
        }
        else
        {
            mat.color = secondary;
        }
    }
    
    
}
