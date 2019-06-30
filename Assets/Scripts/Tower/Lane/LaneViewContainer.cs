using System.Linq;
using UnityEngine;

namespace Tower.Lane
{
    public class LaneViewContainer : MonoBehaviour
    {
        [SerializeField]
        LaneView[] _laneViews;
        [SerializeField]
        DeadLineView _deadLineView;

        public DeadLineView DeadLineView => _deadLineView;

        public LaneView GetLaneViewFromLaneNumber(uint laneNumber)
        {
            var laneView = _laneViews
                .Where(view => view.LaneNumber == laneNumber)
                .FirstOrDefault();
            if (laneView == null)
            {
                throw new System.ArgumentOutOfRangeException($"{laneNumber} isnt contained.");
            }
            return laneView;
        }
    }
}
