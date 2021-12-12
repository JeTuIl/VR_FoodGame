using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.FoodVrApp
{
    /// <summary>
    /// Manage of game, including player health & score
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region PUBLIC_MEMBERS
        /// <summary>
        /// Singleton instance of the game manager
        /// </summary>
        public static GameManager instance;

        #endregion

        #region PRIVATE_MEMBERS

        /// <summary>
        /// Max HP of the player
        /// </summary>
        [SerializeField]
        private int startingLifePoint = 3;

        /// <summary>
        /// Current HP of the player. Game end when this reach 0
        /// </summary>
        [SerializeField]
        private int currentLifePoint;

        /// <summary>
        /// The current score of the player
        /// </summary>
        [SerializeField]
        private int currentScore;

        /// <summary>
        /// The object to activate to display the main menu
        /// </summary>
        [SerializeField]
        private GameObject mainMenuParent;

        /// <summary>
        /// The TextMeshPro displaying the high score in the main menu
        /// </summary>
        [SerializeField]
        private TMPro.TextMeshPro highScoreText;

        /// <summary>
        /// The object to activate to run the main game
        /// </summary>
        [SerializeField]
        private GameObject mainGameParent;

        /// <summary>
        /// The object to activate to display the end game menu
        /// </summary>
        [SerializeField]
        private GameObject endGameParent;

        /// <summary>
        /// The TextMeshPro in the end menu displaying the high score
        /// </summary>
        [SerializeField]
        private TMPro.TextMeshPro endGameText;

        /// <summary>
        /// Reference to the enemy team manager to init when a game start
        /// </summary>
        [SerializeField]
        private EnemyTeamManager enemyTeamManager;


        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// Start a new game (health & score reset)
        /// </summary>
        public void StartGame()
        {
            mainMenuParent.SetActive(false);
            mainGameParent.SetActive(true);
            endGameParent.SetActive(false);

            currentLifePoint = startingLifePoint;
            currentScore = 0;
            enemyTeamManager.Init();
        }

        /// <summary>
        /// Go back to main menu
        /// </summary>
        public void BackToMainMenu()
        {
            mainMenuParent.SetActive(true);
            mainGameParent.SetActive(false);
            endGameParent.SetActive(false);
            UpdateHighScoreText();
        }

        /// <summary>
        /// Remove health from player. May result in end game
        /// </summary>
        public void OnPlayerHurt()
        {
            currentLifePoint--;
            if (currentLifePoint <= 0)
            {
                EndGame();
            }
        }

        /// <summary>
        /// Add score to the player
        /// </summary>
        public void OnEnemyHurt()
        {
            currentScore++;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// Init the GameManager & manage singleton
        /// </summary>
        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                GameManager.Destroy(gameObject);
                return;
            }
            UpdateHighScoreText();

        }

        /// <summary>
        /// Update the high score text in the main menu. Hide the text if no high score has been recorded
        /// </summary>
        private void UpdateHighScoreText()
        {
            if (!PlayerPrefs.HasKey("highSCore") || PlayerPrefs.GetInt("highSCore") <= 0)
            {
                highScoreText.gameObject.SetActive(false);
            }
            else
            {
                highScoreText.gameObject.SetActive(true);
                highScoreText.text = "High Score: "+ PlayerPrefs.GetInt("highSCore");
            }
        }

        /// <summary>
        /// End the game; 
        /// </summary>
        private void EndGame()
        {
            Debug.Log("TODO: EndGame. Score: "+ currentScore);

            mainMenuParent.SetActive(false);
            mainGameParent.SetActive(false);
            endGameParent.SetActive(true);

            endGameText.text = "End of the Game.\n";

            if (currentScore > PlayerPrefs.GetInt("highSCore"))
            {
                endGameText.text += "New high score: " + currentScore+" ! ! !";
                PlayerPrefs.SetInt("highSCore", currentScore);
            }
            else
            {
                endGameText.text += "Your Score: " + currentScore;
            }
        }



        #endregion
    }
}
