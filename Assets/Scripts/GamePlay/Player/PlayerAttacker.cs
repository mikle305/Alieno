using GamePlay.Abilities;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;


        public void Attack()
            => _abilitiesEntity.Call();
    }
}