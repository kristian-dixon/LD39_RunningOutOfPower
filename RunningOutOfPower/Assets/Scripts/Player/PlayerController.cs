﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mCameraGimbal;
    public Transform mCameraTransform;
    public Transform mArms;
    public Camera mCamera;
    public float mHorizontalCameraSensitivity = 100f;
    public float mVerticalCameraSensitivity = 100f;

    public bool mInverted = true;

    public float mMoveSpeed = 4;
    private Rigidbody mRB;

    private bool ShowScreen = true;

    // Use this for initialization
	void Start ()
    {
        mRB = GetComponent<Rigidbody>();
        if(!mCamera)
        {
            if(mCameraTransform)
            {
                mCamera = mCamera.GetComponent<Camera>();
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleLook();
        HandleWalking();

        MoveController();
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
        float RequestedVertAngle = (mCameraTransform.rotation.eulerAngles + new Vector3(verticalRotation, 0, 0)).x;

        //Prevents the andle going above 90 or below 270 as Unity's rotate is weird.
        if(RequestedVertAngle < 90 || RequestedVertAngle > 270)
        {
            mCameraTransform.rotation = Quaternion.Euler(RequestedVertAngle, mCameraTransform.rotation.eulerAngles.y, mCameraTransform.rotation.eulerAngles.z);
        }
    }

    private void MoveController()
    {
        //TODO: Hide/ show controller
        //TODO: Controller sway when moving.
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(ShowScreen)
            {
                Debug.Log("Hello");
                //Screen is in view, user wants it gone

                //TODO: FANCY TRANSITION
                mCamera.fieldOfView = 90;
                mArms.Rotate(50, 0, 0);
            }
            else
            {
                mCamera.fieldOfView = 60;
                mArms.Rotate(-50, 0, 0);
            }

            ShowScreen = !ShowScreen; 
        }
        

        //META: BOTTOM X ROTATION = 50.
        //META: Screen FOV Should be reduced to 60, returned to 90 after.
    }
}
