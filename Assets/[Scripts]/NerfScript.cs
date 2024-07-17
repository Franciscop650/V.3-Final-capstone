using UnityEngine;

public class NerfScript : MonoBehaviour
{
    public GameObject DartPrefab; // The dart prefab to instantiate

    private Transform spawnLocation; // Location where darts will be spawned
    private AudioSource audioSource; // Audio source for playing shoot sound
    private float lastShotTime; // Time of the last shot
    private float dartDelay = 0.2f; // Delay between shots
    [Range(0, 1024), SerializeField] private float dartSpeed = 500f; // Speed of the dart
    AudioSource sound;

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from the GameObject.");
        }
    }

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        // Find the spawn location by tag
        GameObject spawnLocationObj = GameObject.FindGameObjectWithTag("DartSpawn");
        if (spawnLocationObj != null)
        {
            spawnLocation = spawnLocationObj.transform;
        }
        else
        {
            Debug.LogError("No GameObject with tag 'DartSpawn' found in the scene.");
        }
    }

    public void ShootDart()
    {
        sound.Play();
        // Check if DartPrefab is assigned and spawnLocation is valid
        if (DartPrefab == null || spawnLocation == null)
        {
            Debug.LogWarning("DartPrefab or spawnLocation is not set.");
            return;
        }

        // Ensure the time since the last shot is greater than the dart delay
        if (Time.time - lastShotTime < dartDelay)
        {
            return;
        }

        // Instantiate the dart and add force to it
        GameObject dart = Instantiate(DartPrefab, spawnLocation.position, spawnLocation.rotation);
        Rigidbody dartRB = dart.GetComponent<Rigidbody>();

        if (dartRB != null)
        {
            // Apply force to the dart
            dartRB.AddForce(dart.transform.forward * dartSpeed);
            Destroy(dart, 5f); // Destroy the dart after 5 seconds
        }
        else
        {
            Debug.LogWarning("DartPrefab is missing a Rigidbody component.");
        }

        // Play the shoot sound
        ShootDartSound();

        // Update the last shot time
        lastShotTime = Time.time;
    }

    private void ShootDartSound()
    {
        if (audioSource != null)
        {
            float randomPitch = Random.Range(0.8f, 1.2f);
            audioSource.pitch = randomPitch;
            audioSource.Play();
        }
    }
}
