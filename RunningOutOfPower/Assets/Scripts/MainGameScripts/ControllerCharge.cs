using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCharge : MonoBehaviour
{
    float mCurrent = 26;
    float mRateOfLoss = 0.3f;

    public Renderer mConsoleLED;
    public Renderer mChargeLight;

    public bool mCharging = false;

    bool mOn = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mCurrent < 10)
        {

            if (mOn)
            {
                mConsoleLED.material.color = Color.black;
            }
            else
            {
                mConsoleLED.material.color = Color.red;
            }
            mOn = !mOn;
        }
        else if (mCurrent < 25)
        {
            mConsoleLED.material.color = Color.red;

        }
        else
        {
            mConsoleLED.material.color = Color.green;
        }


        if (!mCharging)
        {
            mCurrent -= mRateOfLoss * Time.deltaTime;
            mChargeLight.material.color = Color.white;


        }
        else
        {
            mChargeLight.material.color = Color.yellow;
            mCurrent += 0.5f * Time.deltaTime;
        }
       

	}
}
