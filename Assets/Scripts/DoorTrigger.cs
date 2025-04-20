using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private string workshopSceneName = "Workshop"; // The name of your workshop scene
    
    // Reference to the SceneLoader (optional - can find it dynamically)
    [SerializeField] private SimpleSceneLoader sceneLoader;
    
    // Reference to the Button component on this door (optional)
    private Button doorButton;
    
    private void Start()
    {
        // If we don't have a reference to the SceneLoader, try to find it
        if (sceneLoader == null)
        {
            sceneLoader = FindObjectOfType<SimpleSceneLoader>();
            if (sceneLoader == null)
            {
                Debug.LogWarning("No SimpleSceneLoader found in the scene!");
            }
        }
        
        // Try to get the Button component and add a click listener
        doorButton = GetComponent<Button>();
        if (doorButton != null)
        {
            doorButton.onClick.AddListener(OnDoorButtonClicked);
            Debug.Log("Door button click listener added");
        }
    }
    
    // Handle button clicks
    private void OnDoorButtonClicked()
    {
        Debug.Log("Door button clicked");
        LoadWorkshopScene();
    }
    
    // This is called when a 2D collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected at door, triggering scene transition...");
            LoadWorkshopScene();
        }
    }
    
    private void LoadWorkshopScene()
    {
        if (sceneLoader != null)
        {
            // Set the scene to load in the SimpleSceneLoader
            sceneLoader.sceneToLoad = workshopSceneName;
            
            // Call the LoadScene method
            sceneLoader.LoadScene();
        }
        else
        {
            Debug.LogError("No SimpleSceneLoader reference to trigger scene change!");
        }
    }
}