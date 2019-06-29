using System.Collections.Generic;
using UnityEngine;

namespace Pyke
{
    public class ProjectileView : AbstractView
    {
        [SerializeField]
        DetectView _detectView;

        [SerializeField]
        float _deadTime;

        public IEnumerable<TTargetViewComponent> Shoot<TTargetViewComponent>(Vector3 direction, float speed) where TTargetViewComponent : IUnityView
        {
            _detectView.SetDetectTime(0f, _deadTime);

            foreach (var detectingResult in _detectView.Detect<TTargetViewComponent>())
            {
                transform.position += direction.normalized * speed * Time.deltaTime;
                yield return detectingResult;
            }

            Destroy(gameObject);
        }

        public IEnumerable<TTargetViewComponent> ShootToView<TTargetViewComponent>(IUnityView view, float speed) where TTargetViewComponent : IUnityView
        {
            _detectView.SetDetectTime(0f, _deadTime);

            var angleOfFire = (view.Position - transform.position).normalized;
            transform.localRotation = Quaternion.LookRotation(angleOfFire);
            foreach (var detectingResult in _detectView.Detect<TTargetViewComponent>())
            {
                transform.position += angleOfFire * speed * Time.deltaTime;
                yield return detectingResult;
            }

            Destroy(gameObject);
        }

        public IEnumerable<TTargetViewComponent> ShootToViewAndChase<TTargetViewComponent>(IUnityView view, float speed) where TTargetViewComponent : IUnityView
        {
            _detectView.SetDetectTime(0f, _deadTime);

            foreach (var detectingResult in _detectView.Detect<TTargetViewComponent>())
            {
                var angleOfFire = (view.Position - transform.position).normalized;
                transform.position += angleOfFire * speed * Time.deltaTime;
                transform.LookAt(view.Position);
                yield return detectingResult;
            }

            Destroy(gameObject);
        }
    }
}