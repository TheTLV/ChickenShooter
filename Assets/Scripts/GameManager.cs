using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject gameOverGo;
    public GameObject logoGo;
    public GameObject nameGo;
    public GameObject scoreTextUIGo;
    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver,
    }

    GameManagerState GMState;

    void Start()
    {
        GMState = GameManagerState.Opening;

        playButton.onClick.AddListener(OnPlayButtonClicked);

        UpdateGameManagerState();
    }

    void OnPlayButtonClicked()
    {
        SetGameManagerState(GameManagerState.GamePlay);
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                playButton.gameObject.SetActive(true);
                playerShip.SetActive(false);
                gameOverGo.SetActive(false); // Ẩn màn hình Game Over
                logoGo.SetActive(true);
                nameGo.SetActive(true); 
                enemySpawner.GetComponent<SpawnScript>().UnscheduleEnemySpawner();
                break;

            case GameManagerState.GamePlay:
                scoreTextUIGo.GetComponent<GameScore>().Score = 0;
                playButton.gameObject.SetActive(false);
                playerShip.SetActive(true);
                gameOverGo.SetActive(false); // Ẩn màn hình Game Over
                logoGo.SetActive(false);
                nameGo.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<SpawnScript>().ScheduleEnemySpawner();
                break;

            case GameManagerState.GameOver:
                playButton.gameObject.SetActive(false); // Ẩn nút PLAY
                playerShip.SetActive(false);
                gameOverGo.SetActive(true); // Hiện màn hình Game Over
                logoGo.SetActive(false);
                nameGo .SetActive(false);
                enemySpawner.GetComponent<SpawnScript>().UnscheduleEnemySpawner();
                Invoke(nameof(ChangeToOpeningState), 8f); // Chuyển về Opening sau 8 giây
                break;

        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    void Update()
    {
        // Có thể thêm logic kiểm tra điều kiện thua tại đây
    }
}
