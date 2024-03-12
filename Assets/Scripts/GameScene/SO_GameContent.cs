using System.Collections.Generic;
using UnityEngine;

namespace WHACKGAME
{
    [System.Serializable]
    public class AGameContent
    {
        [SerializeField] private Sprite m_sprite;
        [SerializeField] private float m_score;
        [SerializeField, Range(0.0f, 100.0f)] private float m_percentage;
    }

    [CreateAssetMenu(fileName = "SO_GameContent", menuName = "SO/SO_GameContent", order = 1)]
    public class SO_GameContent : ScriptableObject
    {
        [SerializeField] private List<AGameContent> m_gameContentList = new List<AGameContent>();

        public List<AGameContent> gameContentList => this.m_gameContentList;
    }
}