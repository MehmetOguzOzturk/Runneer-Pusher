using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> activeList = new List<GameObject>();
    public GameObject stickman;
    public CinemachineVirtualCamera cinemachine;

    bool pushForward;
    bool isPushing;
    bool pushBack;

    Vector2 actionPosition;
    float step;
    GameObject player;

    public FieldStatus.colors myColor = FieldStatus.colors.blue;

   
    private void OnTriggerEnter(Collider other)
    {
        IncraseNumber(other);

        MultipleNumber(other);

        DivideNumber(other);

        ChangePlayerColor(other);

        if (other.gameObject.CompareTag("Field"))
        {
            int childCount = other.transform.childCount;
            if (myColor == other.GetComponent<FieldStatus>().myColor)
            {

                for (int i = 0; i < childCount; i++)
                {
                    other.transform.GetChild(0).transform.parent = transform;
                }
                int childCountParent = transform.childCount;
                for (int i = 1; i < childCountParent; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<StickmanController>();
                    transform.GetChild(i).gameObject.GetComponent<StickmanController>().tweenTime= (activeList.Count + 10) / 40;
                    transform.GetChild(i).gameObject.GetComponent<StickmanController>().isSameColor = true;
                    transform.GetChild(i).gameObject.GetComponent<StickmanController>().FindIndex();
                    transform.GetChild(i).GetComponent<Animator>().SetBool("Run", true);
                    transform.GetChild(i).transform.rotation = transform.rotation;

                }

            }
            else if (activeList.Count >= childCount)
            {
                isPushing = true;

                GetComponent<PlayerController>().enabled = false;

                if (other.transform.childCount > 0)
                    transform.position = new Vector3(other.gameObject.transform.GetChild(0).transform.position.x, transform.position.y, transform.position.z);

                foreach (var stickman in activeList)
                {
                    stickman.GetComponent<Animator>().SetBool("Push", true);
                    stickman.GetComponent<Animator>().SetBool("Run", false);
                }

                Animator[] anims = other.gameObject.GetComponentsInChildren<Animator>();

                foreach (var item in anims)
                    item.SetBool("Push", true);

                for (int i = 0; i < childCount; i++)
                    other.transform.GetChild(0).transform.parent = transform;

                pushForward = true;
            }
            else
            {
                GetComponent<PlayerController>().enabled = false;

                if (other.transform.childCount > 0)
                    transform.position = new Vector3(other.gameObject.transform.GetChild(0).transform.position.x, transform.position.y, transform.position.z);

                foreach (var stickman in activeList)
                {
                    stickman.GetComponent<Animator>().SetBool("Push", true);
                    stickman.GetComponent<Animator>().SetBool("Run", false);
                }
                    

                Animator[] anims = other.gameObject.GetComponentsInChildren<Animator>();

                foreach (var item in anims)
                    item.SetBool("Push", true);

                for (int i = 0; i < childCount; i++)
                    other.transform.GetChild(0).transform.parent = transform;

                cinemachine.Follow = null;
                pushBack = true;
                Debug.Log("FAÝL");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            if (isPushing)
            {
                Invoke("PlayerControllerTrue", (activeList.Count + 10) / 40);

                int childCount = transform.childCount;
                for (int i = 1; i < childCount; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<StickmanController>();
                    transform.GetChild(i).gameObject.GetComponent<StickmanController>().tweenTime = (activeList.Count+10)/40;
                    transform.GetChild(i).gameObject.GetComponent<StickmanController>().isPushed = true;
                    transform.GetChild(i).gameObject.GetComponent<StickmanController>().FindIndex();
                    transform.GetChild(i).transform.rotation = transform.rotation;

                }

                foreach (var item in activeList)
                {
                    item.GetComponent<Animator>().SetBool("Run", true);
                    item.GetComponent<Animator>().SetBool("Push", false);
                }
            }


        }
    }

    void PlayerControllerTrue()
    {
        GetComponent<PlayerController>().enabled = true;
    }


    private void ChangePlayerColor(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            myColor = other.gameObject.GetComponent<FieldStatus>().myColor;
            transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.color = other.GetComponent<ParticleSystem>().main.startColor.color;

        }
    }

    private void DivideNumber(Collider other)
    {
        if (other.gameObject.CompareTag("Divider"))
        {
            int currentStack = activeList.Count;
            int add = other.gameObject.GetComponent<DivideNumber>().SetNumber();

            for (int i = currentStack - 1; i >= currentStack / add; i--)
            {
                activeList[i].GetComponent<StickmanController>().enabled = false;
                activeList[i].GetComponent<Animator>().SetTrigger("Die");
                activeList.Remove(activeList[i]);
            }
        }
    }

    private void MultipleNumber(Collider other)
    {
        if (other.gameObject.CompareTag("Multipler"))
        {
            int currentStack = activeList.Count;
            int add = other.gameObject.GetComponent<MultipleNumber>().SetNumber();

            for (int i = currentStack; i < add * currentStack; i++)
            {
                GameObject newStickman = Instantiate(stickman);
                newStickman.GetComponentInChildren<SkinnedMeshRenderer>().material.color = activeList[0].GetComponentInChildren<SkinnedMeshRenderer>().material.color;
                newStickman.transform.position = new Vector3(activeList[0].transform.position.x, newStickman.transform.position.y, newStickman.transform.position.z);
            }
        }
    }

    private void IncraseNumber(Collider other)
    {
        if (other.gameObject.CompareTag("Add"))
        {
            int currentStack = activeList.Count - 1;
            int add = other.gameObject.GetComponent<IncraseNumber>().SetPlusNumber();

            for (int i = currentStack; i < add + currentStack; i++)
            {
                GameObject newStickman = Instantiate(stickman);
                newStickman.GetComponentInChildren<SkinnedMeshRenderer>().material.color = activeList[0].GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            }
        }
    }

    private void Update()
    {
        if (pushForward)
        {
            transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        }
        if (pushBack)
        {
            transform.Translate(Vector3.back * 3 * Time.deltaTime);
        }
    }
}
