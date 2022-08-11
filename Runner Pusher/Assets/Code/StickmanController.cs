using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanController : MonoBehaviour
{

    List<GameObject> activeList;
    GameObject playerParent;
    GameObject target;
    public float lerpSpeed;
    public int myIndex;
    public int index;
    public int followedIndex;
    public bool isSameColor;
    public bool isPushed;
    
    float refFloat = 0;
    public float tweenTime;
    bool stopCheck;


    void Start()
    {
        GetComponent<Animator>().SetBool("Run", true);

        if (!isPushed&&!isSameColor)
        {
            FindIndex();
        }

        followedIndex = (int)((Mathf.Floor((float)(myIndex - 1) / 3) - 1) * 3) + 1;
        if (followedIndex < 0)
        {
            followedIndex = 0;
        }
        target = activeList[followedIndex];
        lerpSpeed = 10;
    }

    public void FindIndex()
    {
        playerParent = GameObject.FindGameObjectWithTag("PlayerParent");
        activeList = playerParent.GetComponent<GameManager>().activeList;
        activeList.Add(gameObject);
        myIndex = activeList.Count - 1;
        index = myIndex - 1;


        if (isPushed )
        {
            stopCheck = true;
            followedIndex = (int)((Mathf.Floor((float)(myIndex - 1) / 3) - 1) * 3) + 1;
            target = activeList[followedIndex];
            float currentLerpSpeed = lerpSpeed + Vector3.Distance(activeList[followedIndex].transform.position, transform.position) * 7;
            float posX = -0.5f + ((float)myIndex % 3) / 2;
            float posZ = -(Mathf.Floor(index / 3) / 2) - 0.45f;
            Debug.Log(tweenTime);
            transform.DOLocalMove(new Vector3(posX, 0, posZ), tweenTime).OnComplete(() =>
            {
                stopCheck = false;
                transform.parent = null;

            });
        }
        if (isSameColor)
        {
            stopCheck = true;
            followedIndex = (int)((Mathf.Floor((float)(myIndex - 1) / 3) - 1) * 3) + 1;
            if (followedIndex < 0)
            {
                followedIndex = 0;
            }
            target = activeList[followedIndex];
            float currentLerpSpeed = lerpSpeed + Vector3.Distance(activeList[followedIndex].transform.position, transform.position) * 7;
            float posX = -0.5f + ((float)myIndex % 3) / 2;
            float posZ = -(Mathf.Floor(index / 3) / 2) - 0.45f;


            transform.DOLocalMove(new Vector3(posX, 0,  posZ), tweenTime).OnComplete(() =>
            {
                stopCheck = false;
                transform.parent = null;
            });
        }
    }


    void Update()
    {
        
        
        if (stopCheck)
            return;
       
        float currentLerpSpeed = lerpSpeed + Vector3.Distance(activeList[followedIndex].transform.position, transform.position) * 10;
        float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x - 0.5f + ((float)myIndex % 3) / 2, ref refFloat, currentLerpSpeed * Time.deltaTime);
        float posZ = -(Mathf.Floor(index / 3) / 2) - 0.45f;
        transform.position = new Vector3(posX, transform.position.y, activeList[0].transform.position.z + posZ);
    }



}
