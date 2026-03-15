using UnityEngine;

namespace ColorCargo.Data
{
    [CreateAssetMenu(fileName = "NewTrainData", menuName = "ColorCargo/Train Data")]
    public class TrainData : ScriptableObject
    {
        public CargoColor color;
        public string hexColor;
        [ColorUsage(true, true)]
        public Color emissionColor;

        public AudioClip activationSound;
        public AudioClip deactivationSound;
        public AudioClip movementSound;
        public AudioClip hornSound;

        public GameObject activationParticlePrefab;
        public GameObject deactivationParticlePrefab;
    }
}
