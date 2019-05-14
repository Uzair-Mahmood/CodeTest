using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePile : MonoBehaviour {

    public ResourceObject pileResourceType;
    public int ammount = 1000;
    public TextMesh pileTitle;
	// Use this for initialization
	void Start () {

        pileTitle.text = "Pile of " + pileResourceType.resourceName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// RequestResource is called by the ResourceCart which is seeking to gather resources from this pile.
    /// </summary>
    /// <returns>The resource.</returns>
    /// <param name="requestingCart">Requesting cart.</param>
    /// <param name="type">Type.</param>
    /// <param name="capacity">Capacity.</param>
    public int RequestResource(ResourceCart requestingCart,ResourceObject type, int capacity)
    {
        if(type == pileResourceType && ammount > 0)
        {
            GiveResources(capacity,requestingCart);
            return 1;
        }
 /*       else if(type != pileResourceType)
        {
            Debug.Log("Requested Resource " + type.resourceName +  " is not Available in pile of " + pileResourceType.resourceName);
            return 2;
        }
        else if(ammount <= 0)
        {
            Debug.Log("Pile of " + pileResourceType.resourceName + " is empty at the moment");
            return 3;
        }
*/
        return 0;
       
    }

    /// <summary>
    /// Once ResourceRequest is successful, GiveResource will be called to finally add resources to the Cart
    /// and remove them from the pile.
    /// </summary>
    /// <param name="capacity">Capacity.</param>
    /// <param name="cart">Cart.</param>
    private void GiveResources(int capacity, ResourceCart cart)
    {
        if(ammount >= capacity)
        {
            ammount = ammount - capacity;
            cart.currentLoad = capacity;
        }
        else if(ammount < capacity)
        {
            cart.currentLoad = ammount;
            ammount = 0;
        }
    }


}
