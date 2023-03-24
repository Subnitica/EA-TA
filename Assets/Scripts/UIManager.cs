using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public Text SpeedTest;
    public TextMeshProUGUI SpeedTest;
    public TextMeshProUGUI UnitText;
    public TextMeshProUGUI GearText;
    public TextMeshProUGUI RPMText;
    public Controls ControlManager;
    public PlayerOptions PlayerOptions;
    public RawImage Upimage;
    public RawImage Downimage;
    public RawImage Leftimage;
    public RawImage Rightimage;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnitText.text = PlayerOptions.UnitString;
        if (PlayerOptions.MPH == false)
        {
            // if player is using km/h convert than display, also convert to int
            float KmhSpeed = ControlManager.SpeedCounter * 1.60934f;
            int myInt = Convert.ToInt32(KmhSpeed);
            SpeedTest.text = myInt.ToString("000");
        }
        else
        {
            //display mph and convert to int
            int myInt = Convert.ToInt32(ControlManager.SpeedCounter);
            SpeedTest.text = myInt.ToString("000");
        }
        //display current gear
        GearText.text = ControlManager.currentGear.ToString();
        //converts rpm to a int
        int RPMint = Convert.ToInt32(ControlManager.rpm);
        RPMText.text = RPMint.ToString();


        //SpeedTest.text = shdfh.ToString();
    }
}
