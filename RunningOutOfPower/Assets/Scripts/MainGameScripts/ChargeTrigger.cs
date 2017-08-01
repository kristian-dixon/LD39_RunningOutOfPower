using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            col.transform.GetComponentInChildren<ControllerCharge>().mCharging = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            col.transform.GetComponentInChildren<ControllerCharge>().mCharging = false;
        }
    }
}
