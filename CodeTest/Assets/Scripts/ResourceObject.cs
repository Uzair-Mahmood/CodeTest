using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data for a specific resource piece that can be used in a game's Economy
/// </summary>
[CreateAssetMenu(menuName = "Data Types/ResourceObject")]
[System.Serializable]
public class ResourceObject : ScriptableObject
{
    public string resourceName = "";
}
