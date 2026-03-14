using UnityEngine;

public class TrackLL : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    private bool isRayHitting = false;

    public void SetRaycastHitLL(bool isHit)
    {
        if (isHit && !isRayHitting)
        {
            isRayHitting = true;
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }
        }
        else if (!isHit && isRayHitting)
        {
            isRayHitting = false;
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false);
            }
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(true);
            }
        }
    }
}