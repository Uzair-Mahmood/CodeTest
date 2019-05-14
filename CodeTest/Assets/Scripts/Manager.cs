using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public List<ResourceObject> resourceObjects = new List<ResourceObject>();
    public List<Text> resourceCountText = new List<Text>();

	// Use this for initialization
	void Start () {

        UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// This function is used to update all the ingredient counters on the UI.
    /// </summary>
    public void UpdateText()
    {
        for (int i = 0; i < resourceObjects.Count; i++)
        {
            resourceCountText[i].text = resourceObjects[i].resourceName + " Count : " + PlayerPrefs.GetInt(resourceObjects[i].resourceName, 0);
        }
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();
        UpdateText();
    }
}
