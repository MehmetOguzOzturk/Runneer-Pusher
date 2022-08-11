using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultipleNumber : MonoBehaviour
{
    public int number;
    public TextMeshPro numberText;

    void Start()
    {
        
        numberText.text = "x" + number.ToString();
    }

    public int SetNumber()
    {
        return number;
    }
}
