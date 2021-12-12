using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Manage the team of enemy, making sure they attack at an interesting pace
    /// </summary>
    public class EnemyTeamManager : MonoBehaviour
    {
        #region PUBLIC_MEMBERS

        #endregion

        #region PRIVATE_MEMBERS

        /// <summary>
        /// Contain all the enemies to manage
        /// </summary>
        [SerializeField]
        private List<EnemyManager> lstEnemies;

        /// <summary>
        /// Time in second between each enemy throw
        /// </summary>
        [SerializeField]
        private float timeBetwenAttack = 8;

        /// <summary>
        /// Time in second between each enemy throw, modulate by difficulty
        /// </summary>
        private float actualTimeBetwenAttack;

        /// <summary>
        /// Time in second between each difficulty jump
        /// </summary>
        [SerializeField]
        private float timeBetwenDifficultyJump = 10;

        /// <summary>
        /// Rate at wich the difficulty change. between 0 & 1. The lower, the quicker.
        /// </summary>
        [SerializeField]
        private float difficultyIncreassRate = 0.9f;

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// Init the team manager
        /// </summary>
        public void Init()
        {
            actualTimeBetwenAttack = timeBetwenAttack;
            foreach (EnemyManager eManager in lstEnemies)
            {
                eManager.currentGatheringDuration = eManager.InitialGatheringDuration;
            }

            StartCoroutine(AttackProcess());
            StartCoroutine(DifficultyProcess());
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        #endregion

        #region PRIVATE_METHODS



        /// <summary>
        /// start enemies to attack at given intervale
        /// </summary>
        /// <returns></returns>
        private IEnumerator AttackProcess()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetwenAttack);
                triggerAttack();
            }

        }

        /// <summary>
        /// Increass difficulty as time pass.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DifficultyProcess()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetwenDifficultyJump);

                //decreass time between throw
                actualTimeBetwenAttack = actualTimeBetwenAttack * difficultyIncreassRate;

                yield return new WaitForSeconds(timeBetwenDifficultyJump);

                //decreass gathering time
                foreach (EnemyManager eManager in lstEnemies)
                {
                    eManager.currentGatheringDuration = eManager.currentGatheringDuration * difficultyIncreassRate;
                }
            }
        }

        /// <summary>
        /// Will order a random ennemy to attack
        /// </summary>
        private void triggerAttack()
        {
            int randomEnemyId = Random.Range(0, lstEnemies.Count);
            if (lstEnemies[randomEnemyId].state == EnemyManager.enemyState.idle)
            {
                lstEnemies[randomEnemyId].StartThrowingCycle();
            }
        }

        #endregion
    }
}
