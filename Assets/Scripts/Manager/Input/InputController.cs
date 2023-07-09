using UnityEngine;

public class InputController : MonoBehaviour
{
    //Singleton
    private static InputController instance = null;

    //Object Data
    private Vector3 direction = Vector3.zero;
    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";
    private float horizontalInput = 0f;
    private float verticalInput = 0f;

    [SerializeField] private Rigidbody rb;

    //Properties
    public static InputController Instance => instance;
    public Vector3 Direction => direction;

    private AudioManager audioManagerInstance;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioManagerInstance = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.gameRunning || !GameState.playerActive)
            return;

        horizontalInput = Input.GetAxis(horizontalAxis);
        verticalInput = Input.GetAxis(verticalAxis);
        direction = new Vector3(horizontalInput, 0f, verticalInput);
        direction = (direction.magnitude > 1f) ? direction.normalized : direction;

        if (direction.magnitude > 0.5f)
        {
            if (audioManagerInstance.walkingAudioSource.isPlaying)
                return;

            audioManagerInstance.PlayWalkingSFX();
        }
        else
        {
            audioManagerInstance.walkingAudioSource.Stop();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}