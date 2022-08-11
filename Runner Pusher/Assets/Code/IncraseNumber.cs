using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncraseNumber : MonoBehaviour
{
    public int number;
    public TextMeshPro numberText ;

    void Start()
    {
       
        numberText.text = "+" + number.ToString(); 
    }

    public int SetPlusNumber()
    {
        return number;
    }
}
