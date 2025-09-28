using UnityEngine;
using UnityEngine.UI;

// Quản lý trạng thái tổng thể của game: mở đầu, đang chơi, kết thúc
public class GameManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Nhạc nền của game
    public Button playButton; // Nút PLAY để bắt đầu game
    public GameObject playerShip; // Tàu của người chơi
    public GameObject enemySpawner; // Đối tượng sinh enemy
    public GameObject gameOverGo; // Giao diện Game Over
    public GameObject logoGo; // Logo hiển thị ở màn hình mở đầu
    public GameObject nameGo; // Tên người chơi hoặc nhóm
    public GameObject namegameGo; // Tên game
    public GameObject scoreTextUIGo; // Giao diện hiển thị điểm
    public GameObject timecounterGo; // Bộ đếm thời gian chơi

    private bool isPaused = false; // Cờ kiểm tra trạng thái tạm dừng

    // Các trạng thái chính của game
    public enum GameManagerState
    {
        Opening,   // Màn hình mở đầu
        GamePlay,  // Đang chơi
        GameOver,  // Kết thúc game
    }

    GameManagerState GMState; // Biến lưu trạng thái hiện tại

    void Start()
    {
        GMState = GameManagerState.Opening; // Khởi động ở trạng thái Opening

        playButton.onClick.AddListener(OnPlayButtonClicked); // Gán sự kiện khi bấm nút PLAY

        UpdateGameManagerState(); // Cập nhật giao diện theo trạng thái hiện tại
    }

    // Hàm xử lý khi người chơi bấm nút PLAY
    void OnPlayButtonClicked()
    {
        if (isPaused)
        {
            Debug.Log("Không thể bắt đầu game khi đang tạm dừng.");
            return; // Ngăn không cho vào GamePlay nếu đang Pause
        }
        SetGameManagerState(GameManagerState.GamePlay); // Chuyển sang trạng thái chơi
    }

    // Hàm cập nhật giao diện và logic theo trạng thái game
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                playButton.gameObject.SetActive(true); // Hiện nút PLAY
                playerShip.SetActive(false); // Ẩn tàu người chơi
                gameOverGo.SetActive(false); // Ẩn màn hình Game Over
                logoGo.SetActive(true); // Hiện logo
                nameGo.SetActive(true); // Hiện tên người chơi
                namegameGo.SetActive(true); // Hiện tên game
                enemySpawner.GetComponent<SpawnScript>().UnscheduleEnemySpawner(); // Dừng sinh enemy
                break;

            case GameManagerState.GamePlay:
                scoreTextUIGo.GetComponent<GameScore>().Score = 0; // Reset điểm
                playButton.gameObject.SetActive(false); // Ẩn nút PLAY
                playerShip.SetActive(true); // Hiện tàu người chơi
                gameOverGo.SetActive(false); // Ẩn màn hình Game Over
                logoGo.SetActive(false); // Ẩn logo
                nameGo.SetActive(false); // Ẩn tên người chơi
                namegameGo.SetActive(false); // Ẩn tên game
                playerShip.GetComponent<PlayerControl>().Init(); // Khởi tạo tàu người chơi
                enemySpawner.GetComponent<SpawnScript>().ScheduleEnemySpawner(); // Bắt đầu sinh enemy
                timecounterGo.GetComponent<TimeCounter>().StartTimeCounter(); // Bắt đầu đếm thời gian
                break;

            case GameManagerState.GameOver:
                timecounterGo.GetComponent<TimeCounter>().StopTimeCounter(); // Dừng đếm thời gian
                playButton.gameObject.SetActive(false); // Ẩn nút PLAY
                playerShip.SetActive(false); // Ẩn tàu người chơi
                gameOverGo.SetActive(true); // Hiện màn hình Game Over
                logoGo.SetActive(false); // Ẩn logo
                nameGo.SetActive(false); // Ẩn tên người chơi
                namegameGo.SetActive(false); // Ẩn tên game
                enemySpawner.GetComponent<SpawnScript>().UnscheduleEnemySpawner(); // Dừng sinh enemy
                Invoke(nameof(ChangeToOpeningState), 8f); // Sau 8 giây chuyển về trạng thái Opening
                break;
        }
    }

    // Hàm chuyển trạng thái game
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState(); // Cập nhật giao diện tương ứng
    }

    // Hàm chuyển về trạng thái mở đầu (sau khi Game Over)
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    // Hàm xử lý khi người chơi bấm phím P để tạm dừng hoặc tiếp tục
    void TogglePause()
    {
        isPaused = !isPaused; // Đảo trạng thái tạm dừng

        if (isPaused)
        {
            Time.timeScale = 0f; // Dừng thời gian game
            backgroundMusic.Pause(); // Dừng nhạc nền
        }
        else
        {
            Time.timeScale = 1f; // Tiếp tục thời gian game
            backgroundMusic.UnPause(); // Phát lại nhạc nền
        }
    }

    void Update()
    {
        // Kiểm tra khi người chơi bấm phím P
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause(); // Gọi hàm tạm dừng hoặc tiếp tục
        }
    }
}
