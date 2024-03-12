using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WHACKGAME
{
    public class InGameUI : MonoBehaviour
    {
        public static InGameUI INSTANCE { get; private set; }

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI m_timerText = null;
        [SerializeField] private TextMeshProUGUI m_scoreText = null;

        [Header("End Game")]
        [SerializeField] private GameObject m_endGameUI = null;
        [SerializeField] private Button m_restartButton = null;

        [Header("Spawn Point")]
        [SerializeField] private Transform m_gameContentSpawnpoint = null;

        [Header("Prefab")]
        [SerializeField] private ButtonGameContent m_gameContentPrefab = null;

        private int m_totalToSpawn = 9;
        private List<ButtonGameContent> m_spawnedGameContentList = new List<ButtonGameContent>();
        public List<ButtonGameContent> spawnedGameContentList => this.m_spawnedGameContentList;

        //=======================================================

        private void Awake()
        {
            if(INSTANCE == null)
            {
                INSTANCE = this;
                this.m_endGameUI.SetActive(false);
                return;
            }

            Destroy(this);
        }

        private void OnEnable()
        {
            this.m_restartButton.onClick.AddListener(this.RestartScene);
        }

        private void OnDisable()
        {
            this.m_restartButton.onClick.RemoveListener(this.RestartScene);
        }

        //=======================================================

        public void UpdateTimerText(float time)
        {
            this.m_timerText.text = time.ToString("0");
        }

        public void UpdateScoreText(int score)
        {
            this.m_scoreText.text = "Score:" + score;
        }

        public void ShowEndGame()
        {
            this.m_endGameUI.SetActive(true);
        }

        public void InitContent()
        {
            if(this.m_spawnedGameContentList == null)
            {
                this.m_spawnedGameContentList = new List<ButtonGameContent>();
            }

            if(this.m_spawnedGameContentList.Count > 0)
            {
                Debug.Log("Not init due to spawned");
                return;
            }

            for(int i = 0; i < this.m_totalToSpawn; i++)
            {
                ButtonGameContent newbtn = Instantiate(this.m_gameContentPrefab, this.m_gameContentSpawnpoint);
                this.m_spawnedGameContentList.Add(newbtn);
            }
        }

        private void RestartScene()
        {
            GameManager.INSTANCE.RestartScene();
        }

        //=======================================================
    }
}