using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mTimeToLive = 5;
    //TODO: Possibly limit bounces

    public float mSpeed = 4.1f;

    Rigidbody2D mRB;

	// Use this for initialization
	void Start ()
    {
        mRB = GetComponent<Rigidbody2D>();
        mRB.velocity = transform.up * mSpeed;

    }

    // Update is called once per frame
    void Update ()
    {
        mTimeToLive -= Time.deltaTime;
        if(mTimeToLive <= 0)
        {
            Destroy(gameObject);
        }

	}

}
