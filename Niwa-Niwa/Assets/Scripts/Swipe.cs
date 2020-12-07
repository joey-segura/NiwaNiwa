using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public bool isDragging = false;
    public Vector2 startTouch, swipeDelta;
    public float touchStart, touchDuration;
    

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        
        //STANDALONE INPUTS
        
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true; 
            startTouch = Input.mousePosition;
            touchStart = Time.time;
            touchDuration = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }

        //MOBILE INPUTS

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
                touchStart = Time.time;
                touchDuration = 0;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        
        //CALCULATE DISTANCE

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2) Input.mousePosition - startTouch;
        }
        
        //DEADZONE CROSS CHECK

        if (swipeDelta.magnitude > 40)
        {
            //WHICH DIRECTION IS SWIPE

            float x = swipeDelta.x;
            float y = swipeDelta.y;
            
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
        touchDuration = Time.time - touchStart;
    }
}
