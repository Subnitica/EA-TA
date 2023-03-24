using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI ButtonText;
    public TextMeshProUGUI GearButtonText;
    public bool GearShiftAutomatic = true;
    public bool MPH = true;
    public string UnitString;
    public string GearUnitString;
    void Start()
    {
        UnitString = "MPH";
        ButtonText.text = UnitString;
        //Gear shifting
        GearUnitString = "Automatic";
        GearButtonText.text = GearUnitString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeUnit()
    {
        //chnages unit
        MPH  ^= true;
        if (MPH)
        {
            UnitString = "mph";
            ButtonText.text = UnitString;
        }
        else
        {
            UnitString = "km/h";
            ButtonText.text = UnitString;
        }
      
    }

    public void ChangeGearShift()
    {
        //changes gear shift
        GearShiftAutomatic ^= true;
        if (GearShiftAutomatic)
        {
            GearUnitString = "Automatic";
            GearButtonText.text = GearUnitString;
        }
        else
        {
            GearUnitString = "Manual";
            GearButtonText.text = GearUnitString;
        }
      
    }


}
