using UnityEngine;

namespace ColorCargo.Core
{
    public interface ITrack
    {
        void SetOccupied(bool isOccupied);
    }

    public class Track : MonoBehaviour, ITrack
    {
        [SerializeField] private GameObject objectToActivate;
        [SerializeField] private GameObject objectToDeactivate;

        private bool _isOccupied = false;

        public void SetOccupied(bool isOccupied)
        {
            if (isOccupied && !_isOccupied)
            {
                _isOccupied = true;
                UpdateState(true);
            }
            else if (!isOccupied && _isOccupied)
            {
                _isOccupied = false;
                UpdateState(false);
            }
        }

        private void UpdateState(bool occupied)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(occupied);
            }
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(!occupied);
            }
        }
    }
}
