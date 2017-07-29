using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatteryState
{
    OK,
    Warning,
    Critical,
    Dead
}


public class SubManager : MonoBehaviour
{
    public List<GameObject> mEnemyPrefabs;
    public Transform mEnemyStore;
    public Transform mBulletStore;

    float mTimeUntilEnemySpawn;
    public float mMinEnemyWait = 1;
    public float mMaxEnemyWait = 5;

    public int mPlayerLives = 5;
    public int mGameScore;

    public float mStartPercentage = 50;
    public float mBaseDrainPercentPerSecond = 1 / 3; //5 minutes for now
    float mDrainPercent;
    public BatteryState mBatteryState;

    public Transform mPlayerShip;

	// Use this for initialization
	void Start ()
    {
        mTimeUntilEnemySpawn = mMinEnemyWait;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(mTimeUntilEnemySpawn <= 0)
        {
            int enemySpawnIndex = Random.Range(0, mEnemyPrefabs.Count);

            //Get player quadrant, spawn outside of it.
            Vector3 playerLocationNorm = Vector3.Normalize(mPlayerShip.localPosition);
            Vector2 PlayerQuad = new Vector2(Mathf.Sign(playerLocationNorm.x), Mathf.Sign(playerLocationNorm.y));


            //Randomize spawn quad
            Vector2 enemyPosition = new Vector2(Random.Range(3, 10) * PlayerQuad.x * -1, Random.Range(2, 5) * PlayerQuad.x * -1);    

            Instantiate(mEnemyPrefabs[enemySpawnIndex], enemyPosition, Quaternion.Euler(Vector3.zero),mEnemyStore);
            //TODO: Something more elegant

            mTimeUntilEnemySpawn = Random.Range(mMinEnemyWait, mMaxEnemyWait);
        }
        else
        {
            mTimeUntilEnemySpawn -= Time.deltaTime;
        }

    }

    public void PlayerDead()
    {
        mPlayerLives--;

        //TODO: GAMEOVER

        DestroyEverything();
    }

    void DestroyEverything()
    {
        for(int i = 0; i < mBulletStore.childCount; i++)
        {
            Destroy(mBulletStore.GetChild(i));
        }

        for(int i = 0; i < mEnemyStore.childCount; i++)
        {
            Destroy(mEnemyStore.GetChild(i));
        }
    }
}
