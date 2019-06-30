using Pyke;
using System.Collections.Generic;
using Tower.Monster;
using UnityEngine;

namespace Tower.Cannon
{
    public class CannonView : AbstractView
    {
        public IEnumerable<MonsterView> Shoot(ProjectileView projectile)
        {
            projectile.transform.SetParent(transform);
            projectile.transform.localPosition = Vector3.zero;
            foreach (var hitTarget in projectile.Shoot<MonsterView>(Vector3.forward, 10f))
            {
                yield return hitTarget;
            }
        }
    }
}
