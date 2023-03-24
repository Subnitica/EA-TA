using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nitrous: MonoBehaviour
{

    public Image TypicalN;
    public float currentNitrous = 50;// our current nitrous
    public float maxNitrous = 50; // max nitrous 
    float maxBurst = 5.0f; // float for max burst nitrous
    float SpeedForLerp; // what the name says lol :P
    public int DelayAmount = 1; // Second count
    private float Timer; // timer for filling nitrous bar
    private bool Waiting = false;
    public bool BurstWaiting = false;// bool to see if we're waiting on the delay between filling burst bars
    public Image Burst;
    public float maxTime = 5f; // max time for burst
    public float timeLeft; // time left for burst nitrous
    public int BurstActives = 0; // how many bursts we have
    bool inUsage = false; //  if burst is currently in use
    public int count = 0;
    private bool[] counted = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        currentNitrous = 0;
        TypicalN.fillAmount = .5f;
        ///////////////////
        ///
        Burst.fillAmount = 0f;
        timeLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNitrous > maxNitrous)
        {
            currentNitrous = maxNitrous;

        }

        

        SpeedForLerp = Time.deltaTime * 5.0f;
        // fills nitrous bar over time
        TimerN();
        //calls nitrous fill function to update
        NitrousFillAmount();
        //player uses typical nitrous and waits before it starts up again filling
        if (Input.GetKey(KeyCode.N))
        {
            currentNitrous = 0;
            
            StartCoroutine(waiter());
        }
        //timer for burst nitrous to deplete over time
        BurstTimer();
       // counts when we fill up a nitrous bar to be used
        Counter();


    }


    void NitrousFillAmount()
    {

      
        TypicalN.fillAmount = Mathf.Lerp(TypicalN.fillAmount, currentNitrous / 100, SpeedForLerp);
       


    }
   

    void TimerN()
    {
        // fills nitrous bar over time
        Timer += Time.deltaTime;

        if (Timer >= DelayAmount)
        {
            Timer = 0f;
         
            currentNitrous++;
        }
    }

    IEnumerator waiter()
    {
        Waiting = true;
        yield return new WaitForSeconds(3);
        Waiting = false;

        
    }

    void BurstTimer()
    {
        // if burst nitrous is above 0 slowly deplete it
        if (timeLeft > 0 &&  BurstWaiting == false)
        {
            timeLeft -= Time.deltaTime;
            Burst.fillAmount = timeLeft / maxTime;
        }
        
    }

    public void AddNitrous(float myInt)
    {
        if (timeLeft < maxBurst && BurstWaiting == false)
        {

            timeLeft += myInt;
        }
          
    }

    public void UseBurst()
    {
        inUsage = true;
        timeLeft = 0;
        Burst.fillAmount = timeLeft / maxTime;
        if (Burst.fillAmount <= 0)
        {
            inUsage = false;

        }
    }

   

   
    void Counter()
    {
        // Check if the input integer is 34, 63, or 88
        int input = Convert.ToInt32(Burst.fillAmount * 100);
        if (input == 34 || input >= 63 && input < 66 || input == 89)
        {
            // Check if the integer has been counted before
            int index = input / 29 - 1;
            if (!counted[index])
            {
                // Add the integer to the count variable and mark it as counted
                if (BurstWaiting == false)
                {
                    
                    BurstActives++;
                    count += input;
                    counted[index] = true;
                    if (inUsage == false)
                    {
                        StartCoroutine(BurstWaiter());

                    }
                }
               
            }
        }

        // Reset the counted array if the count variable is zero
        if (count == 0)
        {
            counted = new bool[3];
        }
    }


    IEnumerator BurstWaiter()
    {
        //waits amount of seconds for player to use nitrous before depleting

        BurstWaiting = true;
        yield return new WaitForSeconds(3);
        BurstWaiting = false;


    }
}
