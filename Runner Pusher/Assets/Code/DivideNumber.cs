using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DivideNumber : MonoBehaviour
{
    public int number;
    public TextMeshPro numberText;

    void Start()
    {
        
        numberText.text = "÷" + number.ToString();
    }

    public int SetNumber()
    {
        return number;
    }
}
