using System.Collections.Generic;
using UnityEngine;

namespace Pyke
{
    [SerializeField]
    [RequireComponent(typeof(DetectView))]
    public class ProjectileView : AbstractView
    {
        [SerializeField]
        float _deadTime;
        [SerializeField]
        ParticleSystemHandleComponent _onDeadVe;

        DetectView _detectView;

        protected override void Awake()
        {
            base.Awake();

            _detectView = GetComponent<DetectView>();
        }

        public IEnumerable<TTargetViewComponent> Shoot<TTargetViewComponent>(Vector3 direction, float speed) where TTargetViewComponent : IUnityView
        {
            _detectView.SetDetectTime(0f, _deadTime);

            foreach (var detectingResult in _detectView.Detect<TTargetViewComponent>())
            {
                transform.position += direction.normalized * speed * Time.deltaTime;
                yield return detectingResult;
            }

            Dead();
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

            Dead();
        }

        public IEnumerable<TTargetViewComponent> Chase<TTargetViewComponent>(IUnityView view, float speed) where TTargetViewComponent : IUnityView
        {
            _detectView.SetDetectTime(0f, _deadTime);

            foreach (var detectingResult in _detectView.Detect<TTargetViewComponent>())
            {
                var angleOfFire = (view.Position - transform.position).normalized;
                transform.position += angleOfFire * speed * Time.deltaTime;
                transform.localRotation = Quaternion.LookRotation(angleOfFire);
                yield return detectingResult;
            }

            Dead();
        }

        public override void Dead()
        {
            _onDeadVe?.Play(transform.position);

            base.Dead();
        }
    }
}