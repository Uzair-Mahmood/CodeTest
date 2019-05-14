using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuilding : MonoBehaviour {

    public string buildingName;
    public List<ResourceProcess> productionProcess = new List<ResourceProcess>();
    public float productionTime = 0.5f;
    public int itemsInProduction = 0;
    public Manager manager;
    public int currentPPId = 0; // PPid = ProductionProcessID
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// This function is responsible for Complete production of the new product.
    /// </summary>
    public void Produce()
    {
        if(productionProcess[currentPPId].CheckIngredientsStock())
        {
            productionProcess[currentPPId].ConsumeIngredients();
            itemsInProduction++;
            StartCoroutine(ProduceInTime());
        }
    }
    /// <summary>
    /// This funciton manages the time required to produce each product.
    /// </summary>
    /// <returns>The in time.</returns>
    IEnumerator ProduceInTime()
    {
        yield return new WaitForSeconds(productionTime);
        productionProcess[currentPPId].Production();
        itemsInProduction--;
        manager.UpdateText();

        // alternatively we can write our own time calculator function in order to keep track of the 
        // time required for each product.
    }

    /// <summary>
    /// This function is used to set id of the required production process.
    /// Based on this id the production building will produce a specific type of stock.
    /// </summary>
    /// <param name="id">Identifier.</param>
    public void SetPPID(int id)
    {
        currentPPId = id;

    }

    /// <summary>
    /// This Function is called by ResourceCart when it reaches the
    /// production building with stock in order to Deposite it.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="ammount">Ammount.</param>
    public void DepositeItemsFromCart(ResourceObject type ,int ammount)
    {
        PlayerPrefs.SetInt(type.resourceName, PlayerPrefs.GetInt(type.resourceName, 0) + ammount);
        manager.UpdateText();
    }


}
