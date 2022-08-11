using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerColor : MonoBehaviour
{
    public FieldStatus.colors myColor;
    private void OnTriggerEnter(Collider other)
    {
        ChangePlayerColors(other);
    }

    private void ChangePlayerColors(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            myColor = other.gameObject.GetComponent<FieldStatus>().myColor;
            GetComponentInChildren<SkinnedMeshRenderer>().material.color = other.GetComponent<ParticleSystem>().main.startColor.color;
        }
    }
}
