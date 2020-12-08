using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    public Transform player;
    public bool inTransit = false, isCharged = false;
    public float distance, rayDistance, moveSpeed;
    
    private bool isBlockedLeft = false, isBlockedRight = false, isBlockedForward = false, isBlockedBack = false;
    private Vector3 desiredPosition;
    private float CHARGETIME = 1.5f;
    
    [SerializeField]
    private LayerMask stopMovment;

    public void ReceiveInput(Vector3 direction, bool left, bool right, bool forward, bool back, float duration)
    {
        Vector3 blockDir = Vector3.zero;

        if (duration > 1)
        {
            isCharged = true;
        }
        else
        {
            isCharged = false;
        }
        
        if (isCharged)
        {
            distance = 2;
            rayDistance = 3;
            moveSpeed = 30;
        }
        else
        {
            distance = 1;
            rayDistance = 1.5f;
            moveSpeed = 10;
        }

        if (left)
        {
            Ray rayLeft = new Ray(player.transform.position, Vector3.left);
            RaycastHit hitLeft;
            if (Physics.Raycast(rayLeft, out hitLeft, rayDistance, stopMovment))
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
            RaycastHit hitRight;
            if (Physics.Raycast(rayRight, out hitRight, rayDistance, stopMovment))
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
            RaycastHit hitForward;
            if (Physics.Raycast(rayForward, out hitForward, rayDistance, stopMovment))
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
            RaycastHit hitBack;
            if (Physics.Raycast(rayBack, out hitBack, rayDistance))
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
            StartCoroutine(MovePlayer(direction, distance));
        }
    }
    
    IEnumerator MovePlayer(Vector3 direction, float distance)
    {
        float scaler = distance * 1.5f;
        Vector3 nextPosition = new Vector3(direction.x, direction.y, direction.z) * scaler;
        nextPosition += player.transform.position;
        
        while (inTransit)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, nextPosition, moveSpeed * Time.deltaTime);
            if (player.transform.position == nextPosition)
            {
                inTransit = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
