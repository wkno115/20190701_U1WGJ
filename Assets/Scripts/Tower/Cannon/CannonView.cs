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
            var createdProjectile = Instantiate(projectile, transform);
            foreach (var hitTarget in createdProjectile.Shoot<MonsterView>(Vector3.forward, 10f))
            {
                yield return hitTarget;
            }
        }
    }
}
