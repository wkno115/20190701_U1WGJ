using Pyke;
using UnityEngine;

namespace Tower.Lane
{
    [RequireComponent(typeof(CollisionHandleComponent))]
    [RequireComponent(typeof(BoxCollider))]
    public class DeadLineView : AbstractView
    {
        protected override void Awake()
        {
            base.Awake();

            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
