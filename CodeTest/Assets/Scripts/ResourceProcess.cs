using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the process of "combining" resourceBatches to create the product
/// </summary>
[CreateAssetMenu(menuName = "Data Types/ResourceProcess")]
[System.Serializable]
public class ResourceProcess : ScriptableObject
{
    /// <summary>
    /// The resource that will be returned from this process 
    /// </summary>
    public ResourceObject product;

    /// <summary>
    /// The ResourceObjects, if any, used to create the product 
    /// </summary>
    public List<ResourceObject> ingredients = new List<ResourceObject>();

    /// <summary>
    /// The amount of each ingredient needed to create the product
    /// </summary>
    public List<int> ingredientAmounts = new List<int>();

    /// <summary>
    /// Where the ingredients are used to create the product
    /// </summary>
    public void Production()
    {
        
        PlayerPrefs.SetInt(product.resourceName, PlayerPrefs.GetInt(product.resourceName, 0) + 1);

    }

    /// <summary>
    /// Checks if the required ingredient is in stock.
    /// </summary>
    public bool CheckIngredientsStock()
    {
        for (int i = 0; i < ingredients.Count; i++)
        {

            if (PlayerPrefs.GetInt(ingredients[i].resourceName, 0) >= ingredientAmounts[i])
            {
                continue;
            }
            else
            {
                return false;
            }
               
        }
        return true;
    }


    /// <summary>
    /// Consumes the required number of ingredients.
    /// </summary>
    public void ConsumeIngredients()
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            PlayerPrefs.SetInt(ingredients[i].resourceName, PlayerPrefs.GetInt(ingredients[i].resourceName, 0) - ingredientAmounts[i]);
        }
    }
}
