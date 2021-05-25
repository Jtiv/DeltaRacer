using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBarValue : MonoBehaviour
{
    public Image image;

    public void SetValue(float Value)
    {
        //Amount based on 
        image.fillAmount = Value;
    }
}
