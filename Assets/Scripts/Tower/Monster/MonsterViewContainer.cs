using UnityEngine;

namespace Tower.Monster
{
    public class MonsterViewContainer : MonoBehaviour
    {
        [SerializeField]
        MonsterView _orkView;
        [SerializeField]
        MonsterView _witchView;
        [SerializeField]
        MonsterView _elfView;

        public MonsterView OrkView => _orkView;
        public MonsterView WitchView => _witchView;
        public MonsterView ElfView => _elfView;
    }
}
