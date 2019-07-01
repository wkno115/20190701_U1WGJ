using System.Collections.Generic;
using UnityEngine;

namespace Pyke
{
    class DetectView : MonoBehaviour
    {
        [SerializeField]
        CollisionHandleComponent _detectCollisionHandler;

        [SerializeField]
        ParticleSystemHandleComponent _onDetectEffect;
        [SerializeField]
        AudioSourceHandleComponent _onDetectSound;


        [SerializeField]
        float _detectStartTime;
        [SerializeField]
        float _detectEndTime;

        public void SetDetectTime(float startTime, float endTime)
        {
            _detectStartTime = startTime;
            _detectEndTime = endTime;
        }

        public IEnumerable<TTargetViewComponent> Detect<TTargetViewComponent>() where TTargetViewComponent : IUnityView
        {
            var attackedTargets = new List<TTargetViewComponent>();
            var timer = 0f;
            while (timer < _detectEndTime)
            {
                timer += Time.deltaTime;
                if (timer > _detectStartTime)
                {
                    foreach (var collider in _detectCollisionHandler.OverlapColliders)
                    {
                        if (collider == null)
                        {
                            continue;
                        }
                        var target = collider.GetComponent<TTargetViewComponent>();
                        if (target != null && !attackedTargets.Contains(target))
                        {
                            _onDetectEffect?.Play(target.Position);
                            _onDetectSound?.Play();

                            attackedTargets.Add(target);
                            yield return target;
                        }
                    }
                }

                yield return default;
            }
        }
    }
}
