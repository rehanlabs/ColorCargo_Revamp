using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorCargo.Core
{
    public class Bomb : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject gameoverpanel; // Reference to the gameover panel
    public AudioSource newBackgroundMusic;



    private void Update()
    {
        // Move the bomb vertically along the tracks
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Train"))
        {
            // Check if the collided object has a Train component
            ColorCargo.Core.Train train = collision.gameObject.GetComponent<ColorCargo.Core.Train>();
            if (train != null)
            {
                // Get the number of available cargos in the train
                int availableCargos = train.GetNextAvailableCargoIndex();

                // Call the DeactivateTwoCargos function in the Train class
                train.DeactivateTwoCargos();

                // Check if the train has one or zero cargo
                if (availableCargos <= 1)
                {
                    if (ColorCargo.Managers.GameManager.Instance != null)
                    {
                        ColorCargo.Managers.GameManager.Instance.GameOver();
                        if (newBackgroundMusic != null) newBackgroundMusic.Play();
                    }
                    else
                    {
                        Time.timeScale = 0;
                        if (newBackgroundMusic != null) newBackgroundMusic.Play();
                        if (gameoverpanel != null) gameoverpanel.SetActive(true);
                    }
                }
            }

            // Destroy the bomb after collision with a train
            Destroy(gameObject);
        }
    }
}
}