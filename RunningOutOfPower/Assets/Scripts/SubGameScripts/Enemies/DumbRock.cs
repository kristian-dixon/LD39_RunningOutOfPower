using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbRock : Enemy
{
    public float mMinSpawnSpeed = 1;
    public float mMaxSpawnSpeed = 5;

    Rigidbody2D mRB;
    // Use this for initialization
	void Start ()
    {
        mRB = GetComponent<Rigidbody2D>();
        Vector2 initalDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        mRB.velocity = initalDirection * Random.Range(mMinSpawnSpeed, mMaxSpawnSpeed);

        Debug.Log(initalDirection);
    }
	
    /*
	public override void BulletHit()
    {
        base.BulletHit();
    }*/
}
