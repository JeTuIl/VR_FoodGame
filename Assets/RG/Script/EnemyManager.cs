using System.Collections;
using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Manage an individual ennemy
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {

        #region PUBLIC_MEMBERS

        /// <summary>
        /// The state an ennemy may be in. Used by the "IA" 
        /// </summary>
        public enum enemyState
        {
            idle,
            gathering,
            throwing
        }

        /// <summary>
        /// The current state this enemy is in. Serialized field for debugging purpose. (will be override at runtime)
        /// </summary>
        [SerializeField]
        public enemyState state;

        /// <summary>
        /// The duration of gathering before throw, modulate by difficulty
        /// </summary>
        public float currentGatheringDuration = 5;

        #endregion

        #region PRIVATE_MEMBERS

        /// <summary>
        /// Reference to the AnimatorController managing the animation of this enemy
        /// </summary>
        [SerializeField]
        private Animator animatorController;

        /// <summary>
        /// Reference to the transform placed at the position wher the projectile should be instantiated
        /// </summary>
        [SerializeField]
        private Transform throwSpawnPoint;

        /// <summary>
        /// The prefab to instantiate when throwing.
        /// </summary>
        [SerializeField]
        private GameObject projectilePrefab;

        /// <summary>
        /// The strenght to apply to the thrown food when throwing it.
        /// </summary>
        [SerializeField]
        private float throwStrenght = 5;

        [SerializeField]
        private Transform target;

        /// <summary>
        /// The original duration of the gathering, before the enemy can throw
        /// </summary>
        [SerializeField]
        private float gatheringDuration = 5;

        /// <summary>
        /// The name of the triger on the animatorController to use to start the gathering animation
        /// </summary>
        [SerializeField]
        private string gatheringTriggerName = "gathering";

        /// <summary>
        /// The name of the triger on the animatorController to use to start the throw animation
        /// </summary>
        [SerializeField]
        private string throwTriggerName = "throw";

        /// <summary>
        /// The name of the triger on the animatorController to use to start the hurt animation
        /// </summary>
        [SerializeField]
        private string hurtTriggerName = "hurt";

        /// <summary>
        /// Get the initial GatheringDuration
        /// </summary>
        public float InitialGatheringDuration { get => gatheringDuration;}

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// Start the throwing cycle, begining with the gathering
        /// </summary>
        public void StartThrowingCycle()
        {
            state = enemyState.gathering;
            animatorController.SetTrigger(gatheringTriggerName);
            StartCoroutine(WaitForGatheringEnd());
        }

        /// <summary>
        /// Called by the animation when the actual throw should happen
        /// </summary>
        public void OnThrow()
        {
            state = enemyState.idle;

            GameObject projectile = GameObject.Instantiate(projectilePrefab);
            projectile.transform.position = throwSpawnPoint.position;
            projectile.transform.LookAt(target);
            Rigidbody rBody = projectile.GetComponent<Rigidbody>();
            rBody.AddForce(projectile.transform.forward * throwStrenght, ForceMode.Impulse);
            Vector3 randomVector = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
            rBody.AddTorque(randomVector * Random.Range(0, 180));
            projectile.GetComponent<DestroyAfterTime>().InvokeDestroyAfterTime();
        }

        /// <summary>
        /// Called when this enemy has been hit by the player. Cancel throwing cycle.
        /// </summary>
        public void OnHurt()
        {
            state = enemyState.idle;
            StopAllCoroutines();
            animatorController.SetTrigger(hurtTriggerName);
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// Init the enemy at the start of the game
        /// </summary>
        private void Start()
        {
            state = enemyState.idle;
        }

        /// <summary>
        /// Wait for the gathering to finished before starting the throw
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitForGatheringEnd()
        {
            yield return new WaitForSeconds(currentGatheringDuration);
            if (state == enemyState.gathering)
            {
                StartThrow();
            }
        }

        /// <summary>
        /// Start the thrwo action: trigger the animation. Actual throw will then be triggered by the animation.
        /// </summary>
        private void StartThrow()
        {
            state = enemyState.throwing;
            animatorController.SetTrigger(throwTriggerName);
        }

        #endregion
    }

}