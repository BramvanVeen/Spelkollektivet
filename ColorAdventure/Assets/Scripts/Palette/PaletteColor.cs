using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteColor : MonoBehaviour
{
    public Color color;
    public void ChooseColor()
    {
        //on buttonclick
        //it should choose color
        Debug.Log("Choose color. "+ color.ToString());
    }
   
}
