using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerOptions PlayerOptions;
    public UIManager UIManager;
    public float SpeedCounter; // Our current Speed
    private float Acceleration = 10f;//How fast we should speed
    private float Decceleration = 20f;// how fast we should slolwy lose speed
    private float Brake = 40f; // our brake speed
    public float minRPM = 100; //min rpm
    public float maxRPM = 10000; // max rpm
    public float rpm; // current rpm
    public int currentGear = 1; // current gear
    public float RpmDanger; // unsafe rpm level
    


    
    public Image DamageMeter; // image for damage


    public float currentHealth = 50;// our health
    public float maxHealth = 50;
    float SpeedForLerp; // our lerp we'll use for changing meter

    public int[] gearThresholds = { 1000, 3000, 4000, 5000, 8000 }; // Define the RPM threshold for each gear

    
    public int maxGears = 5; // Define the maximum number of gears





    void Start()
    {
        currentHealth = 50;
        RpmDanger = .2f;

    }



    // Update is called once per frame
    void Update()
    {
        //gets our lerp speed
        SpeedForLerp = Time.deltaTime * 5.0f;

        Speedometer();
        GearShifter2();
        KeyChannger();


        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } 

       

        FillAmount();
        DamageColor();




    }
    void Speedometer()
    {
        // caculates our speed based off inputs
        if (Input.GetKey(KeyCode.UpArrow))
        {
            UIManager.Upimage.color = Color.gray;
            SpeedCounter += Acceleration * Time.deltaTime;
        }
        else
        {
            SpeedCounter -= Decceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            UIManager.Downimage.color = Color.gray;
            SpeedCounter -= Brake * Time.deltaTime;
        }

        SpeedCounter = Mathf.Clamp(SpeedCounter, 0f, 200);
    }

    public float CalculateWheelRpm(float speedMph, float tireDiameterInches)
    {
        // wheel formula
        float wheelRPM = (speedMph * 63360f) / (tireDiameterInches * 60 * Mathf.PI);
        return wheelRPM;
    }

    public float CalculateEngineRPM(float mph, float transmissionRatio)
    {
        // Calculate wheel RPM
        float wheelRPM = CalculateWheelRpm(mph, 24f);
      
        // Calculate engine RPM
        float engineRPM = (wheelRPM * transmissionRatio);

        return engineRPM;
    }

    void GearShifter2()
    {
        //get rpm
        rpm = CalculateEngineRPM(SpeedCounter, 3.42f);
        //checks what gear mode we're in
        if (PlayerOptions.GearShiftAutomatic == true)
        {
            // Determine which gear the car should be in based on the current RPM.
            for (int i = 0; i < gearThresholds.Length; i++)
            {
                if (rpm < gearThresholds[i])
                {
                    currentGear = i;
                    break;
                }
            }

           
        }
        else
        {
            //Manual gear shiftig
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UIManager.Leftimage.color = Color.gray;
                currentGear--;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                UIManager.Rightimage.color = Color.gray;
                currentGear++;
            }

            currentGear = Mathf.Clamp(currentGear, 0, gearThresholds.Length);
        }


       


        

    }

    void KeyChannger()
    {
        //chnages color of ui keys
        if (!Input.GetKey(KeyCode.UpArrow))
        {

            UIManager.Upimage.color = Color.white;
  
        }
        if (!Input.GetKey(KeyCode.DownArrow))
        {
            UIManager.Downimage.color = Color.white;
        }
        if (!Input.GetKey(KeyCode.LeftArrow))
        {
            UIManager.Leftimage.color = Color.white;
        }
        if (!Input.GetKey(KeyCode.RightArrow))
        {
            UIManager.Rightimage.color = Color.white;
        }

    }


    void FillAmount()
    {
        //lerps what our current health is to what it has changed to
        DamageMeter.fillAmount = Mathf.Lerp(DamageMeter.fillAmount, currentHealth / 100, SpeedForLerp);

    }
    void DamageColor()
    {
        //changes color of health to lerp
     
        DamageMeter.color = Color.Lerp(Color.red, Color.green, currentHealth / maxHealth);
    }

    public void PlusHealth(float myInt)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += myInt;
        }

    }

    public void MinusHealth(float myInt)
    {
        if (currentHealth > 0)
        {
            currentHealth -= myInt;
        }
            
    }
 



}
