using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WHACKGAME
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager INSTANCE { get; private set; }

        [Header("Game Settings")]
        [SerializeField] private float m_gameMaximumTime = 60.0f;
        [SerializeField] private SO_GameContent m_gameContentSO = null;

        private float m_elapsedTime = 0.0f;
        private int m_elapsedScore = 0;
        private bool m_isGameStart = false;
        private Coroutine m_gameLoopCO = null;

        //=======================================================

        private void Awake()
        {
            if (INSTANCE == null)
            {
                INSTANCE = this;
                return;
            }

            Destroy(this);
        }

        private void Start()
        {
            this.StartGame();
        }

        private void Update()
        {
            if (!this.m_isGameStart) return;

            this.m_elapsedTime += Time.deltaTime;

            InGameUI.INSTANCE.UpdateTimerText(this.m_elapsedTime);

            if(this.m_elapsedTime >= this.m_gameMaximumTime)
            {
                this.StopGame();
            }
        }

        //=======================================================

        public void StartGame()
        {
            this.m_elapsedTime = 0.0f;
            this.m_elapsedScore = 0;
            InGameUI.INSTANCE.InitContent();
            this.m_isGameStart = true;

            if(this.m_gameLoopCO != null)
            {
                this.StopCoroutine(this.m_gameLoopCO);
                this.m_gameLoopCO = null;
            }

            this.m_gameLoopCO = this.StartCoroutine(this.GameLoopRandomize());
        }

        public void StopGame()
        {
            if (this.m_gameLoopCO != null)
            {
                this.StopCoroutine(this.m_gameLoopCO);
                this.m_gameLoopCO = null;
            }
            this.m_isGameStart = false;
            InGameUI.INSTANCE.ShowEndGame();
        }
        
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddScore(int score)
        {
            if (!this.m_isGameStart) return;

            this.m_elapsedScore += score;
            InGameUI.INSTANCE.UpdateScoreText(this.m_elapsedScore);
        }

        private IEnumerator GameLoopRandomize()
        {
            float totalPercentage = 0.0f;

            for(int i = 0; i < this.m_gameContentSO.gameContentList.Count; i++)
            {
                totalPercentage += this.m_gameContentSO.gameContentList[i].GetPercentage();
            }

            while(this.m_isGameStart)
            {
                int randomizeButton = -1;
                bool isValid = false;
                while(!isValid)
                {
                    randomizeButton = Random.Range(0, InGameUI.INSTANCE.spawnedGameContentList.Count);
                    isValid = !InGameUI.INSTANCE.spawnedGameContentList[randomizeButton].GetIsButtonPressable();
                    yield return null;
                }

                float randomize = Random.Range(0.0f, totalPercentage);
                float elapsedPercentage = 0.0f;

                for (int i = 0; i < this.m_gameContentSO.gameContentList.Count; i++)
                {
                    if (randomize <= this.m_gameContentSO.gameContentList[i].GetPercentage() + elapsedPercentage)
                    {
                        //init the button with this data
                        InGameUI.INSTANCE.spawnedGameContentList[randomizeButton].InitButton(this.m_gameContentSO.gameContentList[i]);
                        break;
                    }
                    elapsedPercentage += this.m_gameContentSO.gameContentList[i].GetPercentage();
                }

                yield return new WaitForSeconds(2.0f);
            }
        }

        //=======================================================
    }
}