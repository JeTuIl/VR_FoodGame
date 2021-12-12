using System.Collections;
using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Manage the spawning off food, making sure the player alway has something to throw
    /// </summary>
    public class FoodSpawner : MonoBehaviour
    {
        /// <summary>
        /// The prefab to use when instantiating food
        /// </summary>
        [SerializeField]
        private GameObject foodPrefab;

        /// <summary>
        /// The prefab to use for FX.
        /// </summary>
        [SerializeField]
        private GameObject foodFxPrefab;

        /// <summary>
        /// Reference to the last food's transform. Used to determine when a new one should be spwaned
        /// </summary>
        private Transform lastFoodTransform;

        /// <summary>
        /// The time between two check of spwaning
        /// </summary>
        [SerializeField]
        private float refreshTime = 0.5f;

        /// <summary>
        /// No spawn if distance betwen this object and the last instantiated food is inferio to this value
        /// </summary>
        [SerializeField]
        private float respawnDistance = 0.3f;

        /// <summary>
        /// Init this instance
        /// </summary>
        private void Start()
        {
            SpwanFood();
            StartCoroutine(spawningProcess());
        }

        /// <summary>
        /// run continuously, checking if a new spawn should be made
        /// </summary>
        /// <returns></returns>
        IEnumerator spawningProcess()
        {
            while(true)
            {
                if (lastFoodTransform==null || Vector3.Distance(transform.position, lastFoodTransform.position) > respawnDistance)
                {
                    SpwanFood();
                }

                yield return new WaitForSeconds(refreshTime);
            }
        }

        /// <summary>
        /// Spawn a new food throwable for the player to grab
        /// </summary>
        private void SpwanFood()
        {
            GameObject newFood = GameObject.Instantiate(foodPrefab, transform);
            lastFoodTransform = newFood.transform;
            lastFoodTransform.localPosition = Vector3.zero;

            GameObject fx = GameObject.Instantiate(foodFxPrefab, transform);
            fx.transform.localPosition = Vector3.zero;
        }
    }
}
