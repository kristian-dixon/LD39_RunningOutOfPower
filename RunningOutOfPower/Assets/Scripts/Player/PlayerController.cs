using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mCameraGimbal;
    public Transform mCamera;
    public float mHorizontalCameraSensitivity = 100f;
    public float mVerticalCameraSensitivity = 100f;

    public bool mInverted = true;

    public float mMoveSpeed = 4;
    private Rigidbody mRB;

    // Use this for initialization
	void Start ()
    {
        mRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleLook();
        HandleWalking();
    }

    private void HandleWalking()
    {
        float horiVal = Input.GetAxis("Horizontal");
        float vertVal = Input.GetAxis("Vertical");

        //Apply input to respective forwards to get correct movement direction regardless of rotation.
        Vector3 forwardsWalk = vertVal * transform.forward;
        Vector3 sidestep = horiVal * transform.right;

        Vector3 Walk = forwardsWalk + sidestep;
         
        //Prevent the player walking faster than intended
        if (Walk.magnitude > 1)
        {
            Walk.Normalize();
        }

        //Set walk speed to requested movement speed.
        Walk *= Time.deltaTime * mMoveSpeed;

        //Apply
        mRB.MovePosition(mRB.position + Walk);
    }

    private void HandleLook()
    {
        int invert = -1;
        if(mInverted)
        {
            invert = 1;
        }

        //Applying to seperate things prevents camera roll.
        float horizontalRotation = Input.GetAxis("Mouse X") * mHorizontalCameraSensitivity * Time.deltaTime;
        float verticalRotation = Input.GetAxis("Mouse Y") * mVerticalCameraSensitivity * Time.deltaTime * invert;
        mCameraGimbal.Rotate(0, horizontalRotation, 0);

        //Clamp angle to prevent view being confusing
        float RequestedVertAngle = (mCamera.rotation.eulerAngles + new Vector3(verticalRotation, 0, 0)).x;
        Debug.Log(RequestedVertAngle);

        //Prevents the andle going above 90 or below 270 as Unity's rotate is weird.
        if(RequestedVertAngle < 90 || RequestedVertAngle > 270)
        {
            mCamera.rotation = Quaternion.Euler(RequestedVertAngle, mCamera.rotation.eulerAngles.y, mCamera.rotation.eulerAngles.z);
        }
    }
}
