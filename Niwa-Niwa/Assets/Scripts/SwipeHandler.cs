using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    public Swipe swipeControls;
    public Transform player;
    private bool isBlockedLeft = false, isBlockedRight = false, isBlockedForward = false, isBlockedBack = false;
    
    [SerializeField]
    private bool inTransit = false;
    private Vector3 desiredPosition;
    private float CHARGETIME = 2;
    private float RAYDISTANCE = 3;
    

    [SerializeField]
    private LayerMask stopMovment;

    private void Update()
    {
        Ray rayLeft = new Ray(player.transform.position, Vector3.left);
        Debug.DrawRay(player.transform.position, Vector3.left * 3, Color.green);
        RaycastHit hitLeft;
        if (Physics.Raycast(rayLeft, out hitLeft, RAYDISTANCE, stopMovment))
        {
            isBlockedLeft = true;
            Debug.Log(hitLeft.collider.name);
        }
        else
        {
            isBlockedLeft = false;
        }
        
        Ray rayRight = new Ray(player.transform.position, Vector3.right);
        Debug.DrawRay(player.transform.position, Vector3.right * 3, Color.green);
        RaycastHit hitRight;
        if (Physics.Raycast(rayRight, out hitRight, RAYDISTANCE, stopMovment))
        {
            isBlockedRight = true;
        }
        else
        {
            isBlockedRight = false;
        }
        
        Ray rayForward = new Ray(player.transform.position, Vector3.forward);
        Debug.DrawRay(player.transform.position, Vector3.forward * 3, Color.green);
        RaycastHit hitForward;
        if (Physics.Raycast(rayForward, out hitForward, RAYDISTANCE, stopMovment))
        {
            isBlockedForward = true;
        }
        else
        {
            isBlockedForward = false;
        }
        
        Ray rayBack = new Ray(player.transform.position, Vector3.back);
        Debug.DrawRay(player.transform.position, Vector3.back * 3, Color.green);
        RaycastHit hitBack;
        if (Physics.Raycast(rayBack, out hitBack, RAYDISTANCE))
        {
            isBlockedBack = true;
        }
        else
        {
            isBlockedBack = false;
        }

        if (swipeControls.swipeLeft && isBlockedLeft == false)
            desiredPosition += Vector3.left;
        if (swipeControls.swipeRight && isBlockedRight == false)
            desiredPosition += Vector3.right;
        if (swipeControls.swipeUp && isBlockedForward == false)
            desiredPosition += Vector3.forward;
        if (swipeControls.swipeDown && isBlockedBack == false)
            desiredPosition += Vector3.back;

        if (Input.GetMouseButtonUp(0) || (Input.touches.Length > 0 && (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)) && inTransit == false)
        {
            inTransit = true;
            StartCoroutine(MovePlayer(1));
        }
    }
    
    IEnumerator MovePlayer(float distance)
    {
        float scaler = distance * 1.5f;
        
        Vector3 nextPosition = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
        
        while (inTransit)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, nextPosition * scaler, 10f * Time.deltaTime);
            if (player.transform.position == nextPosition * scaler)
            {
                inTransit = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
