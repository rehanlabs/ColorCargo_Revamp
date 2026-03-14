using UnityEngine;

public class CargoContainer : MonoBehaviour
{
    public enum CargoColor { Red, Blue, Yellow, Green}

    public bool enableRed = true;
    public bool enableBlue = true;
    public bool enableYellow = true;
    public bool enableGreen = true;

    public string customHexRed = "#FF3217"; // Custom hex color for Red
    public string customHexBlue = "#0000FF"; // Custom hex color for Blue
    public string customHexYellow = "#FFFF00"; // Custom hex color for Yellow
    public string customHexGreen = "#00B406"; // Custom hex color for Yellow

    public Color customEmissionRed = new Color(135f, 0f, 0f); // Custom emission color for Red (R=1, G=0, B=0)
    public Color customEmissionBlue = new Color(0f, 0f, 1f); // Custom emission color for Blue (R=0, G=0, B=1)
    public Color customEmissionYellow = new Color(1f, 1f, 0f); // Custom emission color for Yellow (R=1, G=1, B=0)
    public Color customEmissionGreen = new Color(10f, 130f, 0f); // Custom emission color for Yellow (R=1, G=1, B=0)


    public AudioSource newBackgroundMusic;


    public float minMoveSpeed = 3f; // Minimum move speed for cargo containers
    public float maxMoveSpeed = 7f;
    private float currentMoveSpeed;


    private CargoColor color;

    public GameObject gameoverpanel;

    private void Start()
    {
        // Randomly assign a color to the cargo container based on enabled colors
        SetRandomColorBasedOnEnabled();

        // Set the cube's color based on the cargo color
        SetColorBasedOnCargo();

        // Set the initial move speed randomly within the specified range
        currentMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        gameoverpanel.SetActive(false);
    }

    private void SetRandomColorBasedOnEnabled()
    {
        bool[] enabledColors = { enableRed, enableBlue, enableYellow, enableGreen };
        int enabledCount = 0;
        foreach (bool enabled in enabledColors)
        {
            if (enabled)
            {
                enabledCount++;
            }
        }

        int randomIndex = Random.Range(0, enabledCount);

        int currentIndex = 0;
        for (int i = 0; i < enabledColors.Length; i++)
        {
            if (enabledColors[i])
            {
                if (currentIndex == randomIndex)
                {
                    color = (CargoColor)i;
                    break;
                }
                currentIndex++;
            }
        }
    }

    private void SetColorBasedOnCargo()
    {
        Renderer cubeRenderer = GetComponent<Renderer>();
        Material[] materials = cubeRenderer.materials; // Get all materials

        foreach (Material material in materials)
        {
            switch (color)
            {
                case CargoColor.Red:
                    ColorUtility.TryParseHtmlString(customHexRed, out Color redColor);
                    material.color = redColor;
                    material.SetColor("_EmissionColor", customEmissionRed);
                    break;
                case CargoColor.Blue:
                    ColorUtility.TryParseHtmlString(customHexBlue, out Color blueColor);
                    material.color = blueColor;
                    material.SetColor("_EmissionColor", customEmissionBlue);
                    break;
                case CargoColor.Yellow:
                    ColorUtility.TryParseHtmlString(customHexYellow, out Color yellowColor);
                    material.color = yellowColor;
                    material.SetColor("_EmissionColor", customEmissionYellow);
                    break;
                case CargoColor.Green:
                    ColorUtility.TryParseHtmlString(customHexGreen, out Color greenColor);
                    material.color = greenColor;
                    material.SetColor("_EmissionColor", customEmissionGreen);
                    break;
            }
        }

        cubeRenderer.materials = materials; // Update the materials array
    }

    private void Update()
    {
        // Move the cargo container vertically along the tracks
        transform.Translate(Vector3.back * currentMoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the cube collides with a train
        if (collision.gameObject.CompareTag("Train"))
        {
            // Get the name of the collided train
            string trainName = collision.gameObject.name;

            // Get a reference to the Train script
            Train train = collision.gameObject.GetComponent<Train>();

            // Check the color of the cube and call ActivateNextCargo on the corresponding train
            switch (color)
            {
                case CargoColor.Red:
                    if (enableRed && trainName == "Red")
                    {
                        train.ActivateNextCargo();
                    }
                    break;
                case CargoColor.Blue:
                    if (enableBlue && trainName == "Blue")
                    {
                        train.ActivateNextCargo();
                    }
                    break;
                case CargoColor.Yellow:
                    if (enableYellow && trainName == "Yellow")
                    {
                        train.ActivateNextCargo();
                    }
                    break;
                case CargoColor.Green:
                    if (enableGreen && trainName == "Green")
                    {
                        train.ActivateNextCargo();
                    }
                    break;
            }

            // Check if the color of the cube does not match the color of the train
            if (train.trainColor != color)
            {
                // Check if the train has cargo
                if (train.GetNextAvailableCargoIndex() == 0)
                {
                    Time.timeScale = 0;
                    AudioSource previousBackgroundMusic = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
                    if (previousBackgroundMusic != null)
                    {
                        previousBackgroundMusic.Stop();
                    }

                    AudioSource bgm2 = GameObject.FindGameObjectWithTag("BGM2").GetComponent<AudioSource>();
                    if (bgm2 != null)
                    {
                        bgm2.Stop();
                    }

                    AudioSource bgm3 = GameObject.FindGameObjectWithTag("BGM3").GetComponent<AudioSource>();
                    if (bgm3 != null)
                    {
                        bgm3.Stop();
                    }

                    // Play the new background music
                    if (newBackgroundMusic != null)
                    {
                        newBackgroundMusic.Play();
                    }

                    gameoverpanel.SetActive(true);
                }
                else
                {
                    train.DeactivateOneCargo();
                }
            }
        }

        // Destroy the cube after the collision
        Destroy(gameObject);
    }

}

