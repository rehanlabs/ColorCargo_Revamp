using UnityEngine;

public class TrainDragDrop : MonoBehaviour
{
    private static bool RaycastPlaneDeployed = false;

    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector2 initialTouchPosition; //gpt added for touch sensitivity
    private TrainDragDrop lastDroppedOnTrain;
    private TrainDragDrop currentTappedTrain; // Keep track of the currently tapped train

    private float[] _colorTrackX = { 168.852f, 169.692f, 169.274f };
    private Vector3 _previousDragPoint = new Vector3();

    public bool enableTouch = true;
    public float dragSmoothness = 2000000f;
    public float snapTolerance = 0.0001f;
    public float redTrackX = 168.852f;
    public float blueTrackX = 169.692f;
    public float greenTrackX = 169.274f;
    public float yellowTrackX = 170.1f;
    public float yIncreaseDuringDrag = 0.1737f;
    public float zDecreaseDuringDrag = 0.297f;


    public void Start()
    {
        if (!RaycastPlaneDeployed)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = this.transform.position - new Vector3(0f, .5f, 0f);
            cube.transform.localScale = new Vector3(40f, .2f, 40f);
            RaycastPlaneDeployed = true;
        }

        dragSmoothness = 2000000f;
        snapTolerance = 0.01f;
    }

    private void Update()
    {
        if (enableTouch)
        {
            HandleTouchInput(); //original code
        }
    }
    //gpt recommended fixed update
    public void FixedUpdate()
    {
        //if (enableTouch)
        //{
        //    HandleTouchInput();
        //}
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchDown(touch.position);
                    break;

                case TouchPhase.Moved:
                    OnTouchDrag(touch.position);
                    break;

                case TouchPhase.Ended:
                    OnTouchUp();
                    break;
            }
        }
    }

    private void OnTouchDown(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                initialPosition = transform.position;
                currentTappedTrain = this; // Store the currently tapped train

                initialTouchPosition = touchPosition; // Store initial touch position for drag calculation

                // Apply adjustments to target position on touch down
                Vector3 touchWorldPosition = hit.point;
                Vector2 dragDirection = Vector2.zero; // Assume no drag initially
                Vector3 targetPosition = ReturnNearestTrackPosition(touchWorldPosition.x);
                targetPosition.y += yIncreaseDuringDrag;
                targetPosition.z += zDecreaseDuringDrag;

                // Apply clamping if necessary
                targetPosition.x = Mathf.Clamp(targetPosition.x, 168.85f, 170.11f);

                // Set the initial position to the adjusted target position
                transform.position = targetPosition;
            }
        }
    }

    private void OnTouchDrag(Vector2 touchPosition)
    {
        if (isDragging && currentTappedTrain != null) // Check if a train is currently tapped
        {
            GetComponent<Collider>().enabled = false;

            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            Vector3 touchWorldPosition = _previousDragPoint;

            if (Physics.Raycast(ray, out hit))
                touchWorldPosition = hit.point;

            Vector3 nearestTrack = ReturnNearestTrackPosition(touchWorldPosition.x);
            Vector3 targetPosition = new Vector3(touchWorldPosition.x, initialPosition.y, initialPosition.z); // ReturnNearestTrackPosition(touchWorldPosition.x);

            targetPosition.y += yIncreaseDuringDrag;
            targetPosition.z += zDecreaseDuringDrag;

            targetPosition.x = Mathf.Clamp(targetPosition.x, 168.85f, 169.69f);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragSmoothness);
        }
    }

    private void OnTouchUp()
    {
        if (isDragging && currentTappedTrain != null)
        {
            GetComponent<Collider>().enabled = true;
            isDragging = false;

            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
            bool isDroppedOnTrain = false;

            foreach (Collider collider in colliders)
            {
                TrainDragDrop otherTrain = collider.GetComponent<TrainDragDrop>();
                if (otherTrain != null && otherTrain != currentTappedTrain)
                {
                    SwapTrainsPosition(otherTrain);
                    isDroppedOnTrain = true;
                    break;
                }
            }

            if (!isDroppedOnTrain)
            {
                SnapToOriginalTrack();
            }

            currentTappedTrain = null; // Reset the currently tapped train
        }
    }

    private void SwapTrainsPosition(TrainDragDrop otherTrain)
    {
        Vector3 tempPosition = otherTrain.transform.position;
        otherTrain.transform.position = initialPosition;
        transform.position = tempPosition;

        initialPosition = transform.position;
        lastDroppedOnTrain = otherTrain;
    }


    private Vector3 ReturnNearestTrackPosition(float currentXPosition)
    {
        float nearestTrackX = 100000.0f; // works as an arbitrary value, can use Mathf.Infinity, dunno performance stats for that

        foreach (float currentTrackPosition in _colorTrackX)
        {
            if (initialPosition.x != currentTrackPosition && Mathf.Abs(currentXPosition - currentTrackPosition) < nearestTrackX)
                nearestTrackX = currentTrackPosition;
        }

        if (Mathf.Abs(currentXPosition - nearestTrackX) <= snapTolerance)
        {
            Debug.Log("Snap: " + (currentXPosition - nearestTrackX));
            return new Vector3(nearestTrackX, initialPosition.y, initialPosition.z);
        }

        Debug.Log("Update: " + (currentXPosition - nearestTrackX));
        return new Vector3(currentXPosition, initialPosition.y, initialPosition.z);

    }

    private void SnapToOriginalTrack()
    {
        transform.position = initialPosition;
    }
}