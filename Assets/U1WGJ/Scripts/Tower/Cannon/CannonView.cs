using Pyke;
using System.Collections.Generic;
using Tower.Monster;
using UnityEngine;

namespace Tower.Cannon
{
    public class CannonView : AbstractView
    {
        [SerializeField]
        ParticleSystemHandleComponent _onFireVe;
        [SerializeField]
        AudioSourceHandleComponent _onFireSe;

        public IEnumerable<MonsterView> Shoot(ProjectileView projectile)
        {
            projectile.transform.SetParent(transform);
            projectile.transform.localPosition = Vector3.zero;
            _onFireVe.Play();
            _onFireSe.Play();
            foreach (var hitTarget in projectile.Shoot<MonsterView>(Vector3.forward, 30f))
            {
                yield return hitTarget;
            }
        }
    }
}
