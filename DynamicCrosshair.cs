using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrosshair : MonoBehaviour {
    public static float spread = 0; //we make it public so we can test it in the game manager
    //const means we can change the values during gamesplay instead of having things hardcoded
    public const int PISTOL_SHOOTING_SPREAD = 20;
    public const int JUMP_SPREAD = 50;
    public const int WALK_SPREAD = 10;
    public const int RUN_SPREAD = 25;

    public GameObject crosshair;
    GameObject topPart;
    GameObject bottomPart;
    GameObject leftPart;
    GameObject rightPart;

    float initailPosition;

    void Start()
    {
        topPart = crosshair.transform.Find("TopPart").gameObject;
        bottomPart = crosshair.transform.Find("BottomPart").gameObject;
        leftPart = crosshair.transform.Find("LeftPart").gameObject;
        rightPart = crosshair.transform.Find("RightPart").gameObject;

        initailPosition = topPart.GetComponent<RectTransform>().localPosition.y;
    }








    private void Update()
    {
        //setting each position simeltaniously
        if(spread != 0)
        {
            topPart.GetComponent<RectTransform>().localPosition = new Vector3(0, initailPosition + spread, 0);
            // negative for bottom, look at those Vectors.  
            bottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -(initailPosition + spread), 0);
            leftPart.GetComponent<RectTransform>().localPosition = new Vector3(-(initailPosition + spread), 0, 0); //x axis
            rightPart.GetComponent<RectTransform>().localPosition = new Vector3(initailPosition + spread, 0, 0);
            spread -= 1;
            //pop tarts would be nice right now
        }//if
    }

}
