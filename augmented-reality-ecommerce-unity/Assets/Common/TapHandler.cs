﻿/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;

public class TapHandler : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    private const float DOUBLE_TAP_MAX_DELAY = 0.5f;//seconds
    private int mTapCount = 0;
    private float mTimeSinceLastTap = 0;
    private MenuAnimator mMenuAnim = null;
	private bool clicked = false;
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start() 
    {
        mTapCount = 0;
        mTimeSinceLastTap = 0;
        mMenuAnim = FindObjectOfType<MenuAnimator>();
    }

    void Update() 
    {
        if (mMenuAnim && mMenuAnim.IsVisible())
        {
            mTapCount = 0;
            mTimeSinceLastTap = 0;
        }
        else
        {
            HandleTap();
        }

#if UNITY_ANDROID
        // On Android, the Back button is mapped to the Esc key
        if (Input.GetKeyUp(KeyCode.Escape))
        {
#if (UNITY_5_2 || UNITY_5_1 || UNITY_5_0)
            Application.LoadLevel("Vuforia-1-About");   
#else // UNITY_5_3 or above
            UnityEngine.SceneManagement.SceneManager.LoadScene("Vuforia-1-About");
#endif
        }
#endif
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void HandleTap()
    {

        if (mTapCount == 1)
        {
            mTimeSinceLastTap += Time.deltaTime;
            if (mTimeSinceLastTap > DOUBLE_TAP_MAX_DELAY)
            {
                // too late for double tap, 
                // we confirm it was a single tap
                OnSingleTapConfirmed();

                // reset touch count and timer
                mTapCount = 0;
                mTimeSinceLastTap = 0;
            }
        }
        else if (mTapCount == 2)
        {
            // we got a double tap
            OnDoubleTap();

            // reset touch count and timer
            mTimeSinceLastTap = 0;
            mTapCount = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mTapCount++;
        }
    }

    protected virtual void OnSingleTapConfirmed()
    {
        CameraSettings camSettings = GetComponentInChildren<CameraSettings>();


        if (camSettings)
        {
            camSettings.TriggerAutofocusEvent();
        }
    }

    protected virtual void OnDoubleTap()
    {
		//Renderer r = gameObject.GetComponent<Renderer> ();

		if (clicked == false) {
			clicked = true;
		} else {
			clicked = false;
		}

//        if (mMenuAnim && !mMenuAnim.IsVisible())
//        {
//            mMenuAnim.Show();
//        }
    }

	void OnGUI() {
		if (clicked == true) {

			string displayText = "Product Title\n";

			displayText = displayText + "\n Details:\n";
			displayText = displayText + "Special Effects\n";
			displayText = displayText + "Textured Print\n";
			displayText = displayText + "Replacement Gaurantee\n";

			displayText = displayText + "\n Price:\n";
			displayText = displayText + "Rs. 1000\n";

			displayText = displayText + "\n See Offers:\n";
			displayText = displayText + "List of offers\n";

			displayText = displayText + "\n See Sellers:\n";
			displayText = displayText + "List of sellers\n";

			float placeY = (1 * Screen.height / 6);
			GUIStyle boxStyle = new GUIStyle("box");
			boxStyle.fontSize = 30;
			GUI.Box (new Rect (Screen.width - 300, 
				placeY, 300, 700), displayText, boxStyle);

			// Add to Cart
			boxStyle.fontSize = 30;
			GUI.backgroundColor = Color.green;
			if(GUI.Button(new Rect(Screen.width - 200,
				(placeY + 600), 250, 70), "Add to Cart")) {
				Debug.Log("Add to Cart Pressed!!!!!!!!");
			}

			// Buy Now
			boxStyle.fontSize = 30;
			GUI.backgroundColor = Color.green;
			if(GUI.Button(new Rect(Screen.width - 200,
				(placeY + 650) ,250, 70), "Buy Now")) {
				Debug.Log("Buy Now Pressed!!!!!!!!");
			}




		}
	}
		

    #endregion // PRIVATE_METHODS
}
