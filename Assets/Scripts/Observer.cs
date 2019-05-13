using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool _IsPlayerInRange;
    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            _IsPlayerInRange = true;
            //Debug.Log("player in range");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            _IsPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (_IsPlayerInRange)
        {
            //make sure the observer can see the player's centre of mass 
            Vector3 direction = player.position - transform.position + Vector3.up;

            //create a ray w an origin and a direction for raycasting whether there are any colliders
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.PlayerCaught();
                    //Debug.Log("player caught");
                }
            }
        }
    }

}
