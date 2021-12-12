using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Manage the projectile & collision aspect of the food
    /// </summary>
    public class FoodProjectile : MonoBehaviour
    {
        /// <summary>
        /// The prefab to instantiate when the food hit the player
        /// </summary>
        [SerializeField]
        private GameObject playerHitFxPrefab;

        /// <summary>
        /// The prefab to instantiate when the food hit an enemy
        /// </summary>
        [SerializeField]
        private GameObject enemyHitFxPrefab;

        /// <summary>
        /// Called when the food physically hit something
        /// </summary>
        /// <param name="collision"></param>
        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag.Equals("MainCamera"))
            {
                GameManager.instance.OnPlayerHurt();

                GameObject fx = GameObject.Instantiate(playerHitFxPrefab);
                fx.transform.position = collision.contacts[0].point;


                GameObject.Destroy(gameObject);
            }

            if (collision.collider.gameObject.tag.Equals("Enemy"))
            {
                collision.collider.gameObject.GetComponentInParent<EnemyManager>().OnHurt();

                GameManager.instance.OnEnemyHurt();

                GameObject fx = GameObject.Instantiate(enemyHitFxPrefab);
                fx.transform.position = collision.contacts[0].point;


                GameObject.Destroy(gameObject);
            }

            if (collision.collider.gameObject.tag.Equals("Start"))
            {
                GameManager.instance.StartGame();

                GameObject fx = GameObject.Instantiate(enemyHitFxPrefab);
                fx.transform.position = collision.contacts[0].point;


                GameObject.Destroy(gameObject);
            }

            if (collision.collider.gameObject.tag.Equals("Next"))
            {
                GameManager.instance.BackToMainMenu();

                GameObject fx = GameObject.Instantiate(enemyHitFxPrefab);
                fx.transform.position = collision.contacts[0].point;


                GameObject.Destroy(gameObject);
            }
        }
    }
}
