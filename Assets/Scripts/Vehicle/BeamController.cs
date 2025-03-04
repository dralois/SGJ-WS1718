﻿using UnityEngine;

public class BeamController : MonoBehaviour {
    //--------------------------------------
    //Renderer
    //--------------------------------------
    private LineRenderer lr;
    //--------------------------------------
    //Targets
    //--------------------------------------
    [SerializeField]
    private UfoController playerCursor;
    [SerializeField]
    private Transform ufo;
    [SerializeField]
    private Transform vehicle1;
    [SerializeField]
    private Transform vehicle2;

    void Start () {
        //--------------------------------------
        //Hole Renderer
        //--------------------------------------
        this.lr = this.GetComponent<LineRenderer>();
        this.lr.positionCount = 12;
	}
	
	void Update () {
        this.lr.SetPosition(11, ufo.position);
        //--------------------------------------
        //Update Positionen
        //--------------------------------------
        if (playerCursor.getCurrentScreen() == Screen.LEFT)
        {
            for(int i = 0; i < 10; i++)
            {
                this.lr.SetPosition(i+1, (vehicle1.position * (10-i)/11.0f + ufo.position* (i+1)/11.0f));
            }
            this.lr.SetPosition(0, vehicle1.position);
            this.transform.LookAt(vehicle1);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                this.lr.SetPosition(i + 1, (vehicle2.position * (10- i) / 11.0f + ufo.position * (i+1) / 11.0f));
            }
            this.lr.SetPosition(0, vehicle2.position);
            this.transform.LookAt(vehicle2);
        }
        //--------------------------------------
        //Wave-Effekt
        //--------------------------------------
        Gradient l_Next = lr.colorGradient;
        GradientAlphaKey[] l_All = l_Next.alphaKeys;
        if (playerCursor.isPulling())
        {
            l_All[1].alpha = 1f;
            l_All[1].time = (l_All[1].time + Time.deltaTime) % 1.0f;     
        }
        else
        {
            l_All[1].alpha = 0.2f;
        }
        l_Next.alphaKeys = l_All;

        this.lr.colorGradient = l_Next;
    }
}