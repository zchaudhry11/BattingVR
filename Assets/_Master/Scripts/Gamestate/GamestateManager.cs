using UnityEngine;

public class GamestateManager : MonoBehaviour
{
    // Timer
    [SerializeField]
    private float GameTimer = 60.0f;

    // Game scoring
    public int PlayerScore { get; set; }
    public float ScoreModifier { get; set; }
    public GameType selectedMode { get; set; }
    public bool hitTarget { get; set; }

    public enum GameType { Batting, Pitching }
    private GameObject ball;
    private BallState ballState;
    private float distTraveled = 0;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballState = ball.GetComponent<BallState>();
    }

    private void Update()
    {
        // If the ball hits the ground, calculate score and reset the ball
        if (selectedMode == GameType.Batting)
        {
            if (ball.transform.position.y <= 0.3f)
            {
                CalculateBattingScore();
            }
        }
        else if (selectedMode == GameType.Pitching) // If playing the pitching mode, calculate score when a target is hit
        {
            if (hitTarget)
            {

            }
        }
    }

    private void FixedUpdate()
    {
        if (GameTimer > 0)
        {
            GameTimer -= Time.deltaTime;
        }
        else
        {
            if (GameTimer < 0)
            {
                GameTimer = 0;
            }

            // End game
            ResetGameState();
        }
    }

    /// <summary>
    /// Calculate the score to add based on the current score modifier.
    /// </summary>
    private void CalculateBattingScore()
    {
        // Calculate new score and add it to the current score
        float distance = ballState.DistTraveled;
        float score = Random.Range(1, 10) + Random.Range(distance, distance*2); // TODO: include distance in calculation

        score *= ScoreModifier;

        PlayerScore += (int)score;

        // Reset score modifier
        ScoreModifier = 1;
    }

    /// <summary>
    /// Calculate the score to add based on the value of the target hit.
    /// </summary>
    /// <param name="targetValue">Score value of the target that was hit.</param>
    public void CalculatePitchingScore(int targetValue)
    {
        PlayerScore += Random.Range(1, 10) * Random.Range(1, 3) * targetValue;
    }

    /// <summary>
    /// Reset game state for next throw.
    /// </summary>
    private void ResetThrowState()
    {
        // Reset all score zone states.

        // Reset ball state
        ball.transform.position = Vector3.zero;
    }

    /// <summary>
    /// Resets the entire game.
    /// </summary>
    private void ResetGameState()
    {

    }
}