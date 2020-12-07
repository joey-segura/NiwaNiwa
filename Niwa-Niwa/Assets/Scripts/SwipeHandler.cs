using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    public Swipe swipeControls;
    public Transform player;
    public bool inTransit = false;
    
    private bool isBlockedLeft = false, isBlockedRight = false, isBlockedForward = false, isBlockedBack = false;
    private Vector3 desiredPosition;
    private float CHARGETIME = 2;
    private float RAYDISTANCE = 1.5f;
    

    [SerializeField]
    private LayerMask stopMovment;

    public void ReceiveInput(Vector3 direction, bool left, bool right, bool forward, bool back)
    {
        Vector3 blockDir = Vector3.zero;
        
        if (left)
        {
            Ray rayLeft = new Ray(player.transform.position, Vector3.left);
            Debug.DrawRay(player.transform.position, Vector3.left * 3, Color.green);
            RaycastHit hitLeft;
            if (Physics.Raycast(rayLeft, out hitLeft, RAYDISTANCE, stopMovment))
            {
                isBlockedLeft = true;
                blockDir = Vector3.left;
            }
            else
            {
                isBlockedLeft = false;
            }
        }

        if (right)
        {
            Ray rayRight = new Ray(player.transform.position, Vector3.right);
            Debug.DrawRay(player.transform.position, Vector3.right * 3, Color.green);
            RaycastHit hitRight;
            if (Physics.Raycast(rayRight, out hitRight, RAYDISTANCE, stopMovment))
            {
                isBlockedRight = true;
                blockDir = Vector3.right;
            }
            else
            {
                isBlockedRight = false;
            }
        }

        if (forward)
        {
            Ray rayForward = new Ray(player.transform.position, Vector3.forward);
            Debug.DrawRay(player.transform.position, Vector3.forward * 3, Color.green);
            RaycastHit hitForward;
            if (Physics.Raycast(rayForward, out hitForward, RAYDISTANCE, stopMovment))
            {
                isBlockedForward = true;
                blockDir = Vector3.forward;
            }
            else
            {
                isBlockedForward = false;
            }
        }

        if (back)
        {
            Ray rayBack = new Ray(player.transform.position, Vector3.back);
            Debug.DrawRay(player.transform.position, Vector3.back * 3, Color.green);
            RaycastHit hitBack;
            if (Physics.Raycast(rayBack, out hitBack, RAYDISTANCE))
            {
                isBlockedBack = true;
                blockDir = Vector3.back;
            }
            else
            {
                isBlockedBack = false;
            }
        }

        if (direction != blockDir)
        {
            inTransit = true;
            StartCoroutine(MovePlayer(direction, 1));
        }
    }
    
    IEnumerator MovePlayer(Vector3 direction, float distance)
    {
        float scaler = distance * 1.5f;
        Vector3 nextPosition = new Vector3(direction.x, direction.y, direction.z) * scaler;
        nextPosition += player.transform.position;
        
        while (inTransit)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, nextPosition, 10f * Time.deltaTime);
            if (player.transform.position == nextPosition)
            {
                inTransit = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
