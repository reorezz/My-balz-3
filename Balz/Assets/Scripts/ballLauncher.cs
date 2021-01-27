using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLauncher : MonoBehaviour
{

    List<ball> ballContanier = new List<ball>();
    FloorTouch floorTouch;

    [SerializeField]
    ball ballPrefab;

    Vector3 startDragpos, EndDragPos, CurrDragPos;
    Vector2 launchDirection;
    public bool isLaunching, allRecived;

    public int CurrBallCount, totalBallCount;
    public int RecivedBallCount;
    public int  newAddedBallCount;

    // newAddedBallCount NewAddedBallCount {get; set;}

    public GameObject LaunchPad,LaunchpadBeforeLauch;
    public Vector2 NewLaunchPadpos,oldLaunchPadPos;

    private void Awake()
    {
        floorTouch = FindObjectOfType<FloorTouch>();
    }

    private void Start()
    {
        if(ballContanier.Count==0)
        {
            CreatBall();
            
        }
        ballContanier[0].gameObject.SetActive(true);
        CurrBallCount = totalBallCount = RecivedBallCount = ballContanier.Count;
        newAddedBallCount = 0;
        isLaunching = false;
        NewLaunchPadpos = (Vector2)LaunchPad.transform.position;
        LaunchPad.SetActive(true);
        
    }

   

    private void Update()
    {
        if(isLaunching)
        {
            LaunchpadBeforeLauch.SetActive(true);
        }
        else
        {
            LaunchpadBeforeLauch.SetActive(false);
            LaunchpadBeforeLauch.transform.position = NewLaunchPadpos;
        }
        CurrBallCount = RecivedBallCount;
        if(CurrBallCount == totalBallCount )
        {
            allRecived = true;
            floorTouch.firstTouch = false;
           
            GetLaunchInputs();

        }
        LaunchPad.transform.position = NewLaunchPadpos;


    }
    


    void GetLaunchInputs()
    {
        CurrDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + transform.forward*10f;
        if (Input.GetMouseButtonDown(0))
        {
            startDragpos = CurrDragPos;
        }
        if(Input.GetMouseButton(0))
        {
            EndDragPos = CurrDragPos;
        }
        if(Input.GetMouseButtonUp(0))
        {
           StartCoroutine( "LaunchAllBall");
        }
    }
    IEnumerator LaunchAllBall()
    {
        launchDirection = -(Vector2)(EndDragPos - startDragpos);

        allRecived = false;
        if (launchDirection.y > 0.1f)
        {  
            if(newAddedBallCount!=0)
            {
                for(int i=0;i<newAddedBallCount;i++)
                {
                    CreatBall();
                }
                newAddedBallCount = 0;
                totalBallCount = ballContanier.Count;
            }
            isLaunching = true;
            RecivedBallCount = 0;//while launching return ball is zer0
            foreach(ball b in ballContanier)
            {
                b.gameObject.transform.position = LaunchpadBeforeLauch.transform.position;//NewLaunchPadpos;
                b.gameObject.SetActive(true);
                b.gameObject.GetComponent<Rigidbody2D>().AddForce(launchDirection);
                CurrBallCount--;//reduce the curr ball count by 1 as we launch each ball

                yield return new WaitForSeconds(0.15f);
            }
           // if(CurrBallCount ==0)
            //{
                isLaunching = false;//
                LaunchPad.SetActive(false);
                
            //}

        }

    }


    private void CreatBall()
    {
        ball newBall = Instantiate(ballPrefab);
        newBall.gameObject.SetActive(false);
        ballContanier.Add(newBall);

    }





}
