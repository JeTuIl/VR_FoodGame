using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Handle the destruction of the food after it is thrown
    /// </summary>
    public class DestroyAfterTime : MonoBehaviour
    {
        /// <summary>
        /// The duration between the throw and the deletion of the object
        /// </summary>
        [SerializeField]
        private float duration = 5;

        /// <summary>
        /// Once invoked, will destoy the object after the given duration.
        /// </summary>
        public void InvokeDestroyAfterTime()
        {
            Invoke("DestroyObject", duration);
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        private void DestroyObject()
        {
            GameObject.Destroy(gameObject);
        }
    }
}
