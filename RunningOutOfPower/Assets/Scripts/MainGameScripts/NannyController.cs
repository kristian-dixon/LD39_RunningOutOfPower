using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapNode
{
    public float mTimeLastVisted;
    public Vector3 mPostion;

    public MapNode(Vector3 pPosition)
    {
        mPostion = pPosition;
        mTimeLastVisted = 0;
    }
}

public class NannyController : MonoBehaviour
{
    NavMeshAgent mAgent;

    public Transform mRoomContainer;

    private List<MapNode> mNodes;
    private int currentTarget;

    private NannyLook mNanLook;
    public SubManager mSubGame;
    float height;
    float mTimeLastSeenPlayer;
    bool mChasingPlayer = false;

	// Use this for initialization
	void Start ()
    {
        mAgent = GetComponent<NavMeshAgent>();

        mNodes = new List<MapNode>();

        if(mRoomContainer)
        {
            for(int i = 0; i < mRoomContainer.childCount; i++)
            {
                mNodes.Add(new MapNode(mRoomContainer.GetChild(i).position));
            }
        }
        mNanLook = GetComponent<NannyLook>();
        mAgent.SetDestination(ChooseNewTarget());
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(mAgent.isStopped);

        if (mAgent.remainingDistance == 0 )
        {
            if(mChasingPlayer)
            {
                //Game Over!
                mSubGame.GameOver();
                
            }
            else
            {
                mNodes[currentTarget].mTimeLastVisted = Time.time;
                mAgent.SetDestination(ChooseNewTarget());
            }
            
        }

        if(mChasingPlayer && Time.time - mTimeLastSeenPlayer > 5)
        {
            PlayerLost();
        }

    }

    Vector3 ChooseNewTarget()
    {
        int bestChoice = -1;
        float longestTimeWithoutVisit = -1;

        //Go through list
        //Find node that hasn't been visited for the longest

        //TODO: Factor in distance
        float currentTime = Time.time;

        for(int i = 0; i < mNodes.Count; i++)
        {
            float timeSinceLastVisit = currentTime - mNodes[i].mTimeLastVisted;

            if(timeSinceLastVisit > longestTimeWithoutVisit)
            {
                longestTimeWithoutVisit = timeSinceLastVisit;
                bestChoice = i;
            }
            else if(timeSinceLastVisit == longestTimeWithoutVisit)
            {
                float thisDistance = Vector3.Distance(mNodes[i].mPostion, transform.position);
                if(mNodes[i].mPostion.y != height)
                {
                    thisDistance *= 100f;
                }
                

                float otherDistance = Vector3.Distance(mNodes[bestChoice].mPostion, transform.position);
                if (mNodes[bestChoice].mPostion.y != height)
                {
                    otherDistance *= 100f;
                }

                if (thisDistance < otherDistance)
                {
                    bestChoice = i;
                }
            }
        }
        currentTarget = bestChoice;
        height = mNodes[bestChoice].mPostion.y;
        return mNodes[bestChoice].mPostion;
    }

    public void PlayerFound(Vector3 PlayerPosition)
    {
        mAgent.speed += 0.2f;
        mAgent.SetDestination(PlayerPosition);
        mTimeLastSeenPlayer = Time.time; 
        mChasingPlayer = true;
    }

    public void PlayerLost()
    {
        mChasingPlayer = false;
        mAgent.speed -= 0.2f;
    }
}
