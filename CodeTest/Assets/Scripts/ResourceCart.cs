using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResourceCart : MonoBehaviour {

    ResourcePile targetResourcePile;
    public ResourceObject cartResourceType;
    public ProductionBuilding targetProductionBuilding;

    public int cartCapacity = 10;
    public int currentLoad;
    int currentCapacity = 10;
 
    NavMeshAgent cartAgent;

    int cartState ;



	// Use this for initialization
	void Start () {

        cartAgent = gameObject.GetComponent<NavMeshAgent>();
        cartAgent.isStopped = true;


	}
	
    /*cartState :
     * 0 = Empty and doing nothing
     * 1 = Has target destination of Resource Pile and moving towards it
     * 2 = Has reached the Resource Pile and Requesting for Resources
     * 3 = Has received resources and moving back towards the Production Building
     * 4 = Has reached Production Building and depositing the resources
     * 
     */
        
	// Update is called once per frame
	void Update () {
		
       

	}

    void LateUpdate()
    {
        if (cartState == 0)
        {

            if (LookForRequiredPile())
            {
                cartAgent.SetDestination(targetResourcePile.transform.position);
                cartAgent.isStopped = false;
                cartState = 1;
            }

        }

        else if(GetDistance() <= cartAgent.stoppingDistance)
        {
            if(cartState == 1)
            {
                //Debug.Log("Cart Reached The Resource Pile");
                cartAgent.isStopped = true;
                cartState = 2;
                RequestResource();
            }
            else if(cartState == 3)
            {
                cartState = 4;
                //Debug.Log("Cart Reached The Production Building");
                targetProductionBuilding.DepositeItemsFromCart(targetResourcePile.pileResourceType,currentLoad);
                currentLoad = 0;
                currentCapacity = cartCapacity;
                //Debug.Log("Cart Deposited the Resources");

                cartState = 0;
            }
           
        }

    }
     

    /// <summary>
    /// Returns the distance between the Cart and its Destination
    /// </summary>
    /// <returns>The distance.</returns>
    float GetDistance()
    {
        return Vector3.Distance(transform.position,cartAgent.destination);

        // at the moment we are using this simple way of checking if the cart has reached its destination.
        // it can be (should be) optimised further by adding one or two more checks to make sure the 
        // cart doesn't stuck near its destination.
    }

    /// <summary>
    /// This function checks if the type of ResourcePile required by the Cart is Available with Resources.
    /// </summary>
    /// <returns><c>true</c>, if for required pile was looked, <c>false</c> otherwise.</returns>
    bool LookForRequiredPile()
    {

        ResourcePile[] objects = FindObjectsOfType<ResourcePile>();

        foreach(ResourcePile resource in objects)
        {
            if(resource.pileResourceType == cartResourceType)
            {
                if(resource.ammount > 0)
                {
                    targetResourcePile = resource;
                    //Debug.Log("Found a Resource Pile of Type : " + targetResourcePile.pileResourceType);
                    return true;
                }
            }
        }

        //Debug.Log("No Resource Pile of Type : " + cartResourceType.resourceName + " found with available resources!" );
        return false;
    }

    /// <summary>
    /// This function is used by the Cart to Request resources from the target ResourcePile.
    /// </summary>
    void RequestResource()
    {
        int result;

        result = targetResourcePile.RequestResource(gameObject.GetComponent<ResourceCart>(), cartResourceType, currentCapacity);

        if(result == 1)
        {
            currentCapacity = currentCapacity - currentLoad;
            ReturnToProductionBuilding();
        }
/*        else if(result == 2 || result == 3)
        {
            // look for another resource pile. it would be benificial if while looking for a resource pile -
            // we check if the pile has resources higher than 0 or not. 
        }
*/
    }

    /// <summary>
    /// This function is called to request the cart to return to the Production Building after receiving the Ingredients.
    /// </summary>
    void ReturnToProductionBuilding()
    {
        cartAgent.SetDestination(targetProductionBuilding.transform.position);
        cartAgent.isStopped = false;
        cartState = 3;
        //Debug.Log("Cart is heading towards the production building");
    }
        
}
