using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WHACKGAME
{
    [RequireComponent(typeof(Button))]
    public class ButtonGameContent : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image m_buttonImage = null;
        [SerializeField] private Animator m_animator = null;

        private AGameContent m_data = null;
        private Button m_button = null;

        private const string IN = "In";
        private const string OUT = "Out";

        private int m_animationIn = -1;
        private int m_animationOut = -1;

        //=======================================================}

        private void Start()
        {
            this.m_animationIn = Animator.StringToHash(IN);
            this.m_animationOut = Animator.StringToHash(OUT);
        }

        private void OnEnable()
        {
            if (this.m_button == null)
            {
                this.m_button = this.GetComponent<Button>();
            }
            this.m_button.onClick.AddListener(this.OnButtonClick);
        }

        private void OnDisable()
        {
            this.m_button.onClick.RemoveListener(this.OnButtonClick);
        }

        //=======================================================

        public bool GetIsButtonPressable() => this.m_button.interactable;

        public void InitButton(AGameContent data)
        {
            this.m_data = data;

            this.m_buttonImage.sprite = data.GetSprite();

            this.m_animator.SetTrigger(this.m_animationIn);
        }

        private void OnButtonClick()
        {
            this.m_animator.SetTrigger(this.m_animationOut);

            GameManager.INSTANCE.AddScore(this.m_data.GetScore());
        }

        //=======================================================
    }
}