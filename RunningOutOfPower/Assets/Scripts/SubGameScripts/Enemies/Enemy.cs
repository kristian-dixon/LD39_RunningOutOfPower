using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int mHealth = 3;
    protected int mScore = 100;

    public SubManager mManager;

    virtual public void BulletHit()
    {
        mHealth--;
        if(mHealth <= 0)
        {
            mManager.UpdateScore(mScore);
            Destroy(gameObject);
        }
    }
}
