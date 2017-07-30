using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NannyLook : MonoBehaviour
{
    public Transform mPlayer;
    public Transform mEyes;
    public float mSightRange = 5;
    public float mSightAngle = 0.7f;

	// Use this for initialization
	void Start ()
    {
		if(!mEyes)
        {
            mEyes = transform;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Vector3.Distance(mPlayer.position, transform.position) < mSightRange)
        {
            if(Vector3.Dot(mPlayer.position.normalized, transform.forward.normalized) > mSightAngle)
            {
                Ray ray = new Ray(mEyes.position, (mPlayer.position - mEyes.position).normalized);
                RaycastHit rayResult;

                Physics.Raycast(ray, out rayResult, mSightRange);
                if(rayResult.transform.CompareTag("Player"))
                {
                    Debug.Log("I CAN SEE YOU");
                    Debug.DrawLine(mEyes.position, mPlayer.position, Color.red, 5f);

                }

            }
        }
	}


}
