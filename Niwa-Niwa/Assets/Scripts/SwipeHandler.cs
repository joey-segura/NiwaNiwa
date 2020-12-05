using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    public Swipe swipeControls;
    public Transform player;
    public bool isBlocked_0 = false;
    public bool isBlocked_1 = false;
    private Vector3 desiredPosition;

    private void Update()
    {
        if (swipeControls.swipeLeft)
            desiredPosition += Vector3.left;
        if (swipeControls.swipeRight)
            desiredPosition += Vector3.right;
        if (swipeControls.swipeUp)
            desiredPosition += Vector3.forward;
        if (swipeControls.swipeDown)
            desiredPosition += Vector3.back;
        
        player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition * 3, 15f * Time.deltaTime);
    }
}
