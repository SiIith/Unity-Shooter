using UnityEngine;
using System.Collections.Generic;
 
public static class SavedPositionManager // Static class to remember player positions per scene.
{
    public static Dictionary<int, Vector3> savedPositions = new Dictionary<int, Vector3>{
        [1] = new Vector3(5,0,-20),
        [2] = new Vector3(5,0,-20)
    };
}