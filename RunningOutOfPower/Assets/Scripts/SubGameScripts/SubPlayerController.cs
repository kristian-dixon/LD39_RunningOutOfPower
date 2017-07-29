using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerController : MonoBehaviour
{

    public Transform mBullet;
    public Transform mThrusterLeft;
    public Transform mThrusterRight;

    Transform mBulletContainer;
    public float mTimeBetweenShots = 0.25f;
    float mCannonCooldown = 0;

    Rigidbody2D mRB;
    // Use this for initialization
    void Start()
    {
        mBulletContainer = new GameObject("PlayerBullets").transform;
        //mBulletContainer.parent = this.transform;

        mRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            mRB.AddForceAtPosition(transform.up, mThrusterLeft.position);
        }
        if (Input.GetKey(KeyCode.X))
        {
            mRB.AddForceAtPosition(transform.up, mThrusterRight.position);
        }
        

        if (mCannonCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(mBullet, transform.position, transform.rotation, mBulletContainer);
                mCannonCooldown = mTimeBetweenShots;
            }
        }
        else
        {
            mCannonCooldown -= Time.deltaTime;
        }

        //TODO: UPDATE THESE WITH PROPER INPUT MANAGEMENT
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Enemy"))
        {
            Debug.Log("Dead mate");
        }
    }
}
