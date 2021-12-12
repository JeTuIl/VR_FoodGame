using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Throw this object when the application run in the editor
    /// </summary>
    public class EditorThrow : MonoBehaviour
    {
        /// <summary>
        /// reference to the rigibody on wich to apply the throw
        /// </summary>
        [SerializeField]
        private new Rigidbody rigidbody;

        /// <summary>
        /// The strenght at wich to throw the object
        /// </summary>
        [SerializeField]
        private float strenght=5;

        /// <summary>
        /// Called when the object has been thrown
        /// </summary>
        public void OnThrow()
        {
            if (Application.isEditor)
            {
               rigidbody.AddRelativeForce(Vector3.forward * strenght, ForceMode.Impulse);
            }
        }
    }
}
