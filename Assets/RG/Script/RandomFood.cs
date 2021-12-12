using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Manage the appearance of the food prefab by selecting a random child to display
    /// </summary>
    public class RandomFood : MonoBehaviour
    {
        /// <summary>
        /// Init this instance
        /// </summary>
        void Start()
        {
            int childId = Random.Range( 0, transform.childCount );
            transform.GetChild(childId).gameObject.SetActive(true);
        }
    }
}
