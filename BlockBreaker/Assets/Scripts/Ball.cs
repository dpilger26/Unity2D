using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle;
    [SerializeField] float xLaunchVelocity = 0f;
    [SerializeField] float yLaunchVelocity = 0f;
    [SerializeField] AudioClip[] soundFXs;
    [SerializeField] float randomFactor = 0.2f;

    // state
    private Vector2 paddleToBallOffset;
    private bool hasStarted = false;

    // cached component references
    private AudioSource audioSource;
    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    private void Start()
    {
        if (paddle == null)
        {
            throw new MissingComponentException("Paddle object has not been added.");
        }

        float xOffset = transform.position.x - paddle.transform.position.x;
        float yOffset = transform.position.y - paddle.transform.position.y;
        paddleToBallOffset = new Vector2(xOffset, yOffset);

        audioSource = GetComponent<AudioSource>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    // launch the ball when the player clicks the mouse button
    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rigidBody2D.velocity = new Vector2(xLaunchVelocity, yLaunchVelocity);
        }
    }

    // lock the ball to the paddle at the start of the game until the player
    // launches it with a mouse click
    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + paddleToBallOffset;
    }

    // play a sound when the ball hits something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            PlayAudio();
            TweakVelocity();
        }
    }

    private void TweakVelocity()
    {
        var velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        rigidBody2D.velocity += velocityTweak;
    }

    private void PlayAudio()
    {
        if (soundFXs.Length > 0)
        {
            var audioIdx = UnityEngine.Random.Range(0, soundFXs.Length);
            audioSource.PlayOneShot(soundFXs[audioIdx]);
        }
    }
}
