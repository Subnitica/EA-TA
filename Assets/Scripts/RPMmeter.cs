using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPMmeter : MonoBehaviour
{
    public Controls ControlsManager;

    public Image DangerRpm;
    public Image SafeRpm;
   



    // Start is called before the first frame update
    void Start()
    {
        //sets bad rpm to what it is set to in controller
        DangerRpm.fillAmount = ControlsManager.RpmDanger;
        //sets rpm to 0
        SafeRpm.fillAmount = 0f;
     
    }

    // Update is called once per frame
    void Update()
    {
        //changes rpm based off the rpm in controller
        SafeRpm.fillAmount = ControlsManager.rpm / 10000;


    }



}
