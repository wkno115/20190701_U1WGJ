using System.Linq;
using UnityEngine;

namespace Tower.Monster
{
    public class MonsterViewContainer : MonoBehaviour
    {
        [SerializeField]
        MonsterView[] _monsterViews;

        public MonsterView GetMonsterViewFromMonsterType(MonsterType monsterType)
        {
            var monsterView = _monsterViews
                .Where(view => view.MonsterType == monsterType)
                .FirstOrDefault();
            if (monsterView == null)
            {
                throw new System.ArgumentOutOfRangeException($"{monsterView} isnt contained.");
            }
            return monsterView;
        }
    }
}
