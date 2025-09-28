using UnityEngine;
using TMPro; // Thư viện hỗ trợ TextMeshPro (hiển thị chữ đẹp và chuyên nghiệp hơn UI Text)

// Script quản lý điểm số và cập nhật giao diện điểm
public class GameScore : MonoBehaviour
{
    private TextMeshProUGUI scoreTextUI; // Biến lưu tham chiếu đến thành phần hiển thị điểm
    private int score; // Biến lưu điểm hiện tại

    // Thuộc tính công khai để truy cập và cập nhật điểm từ bên ngoài
    public int Score
    {
        get => score; // Trả về điểm hiện tại
        set
        {
            score = value; // Gán điểm mới
            UpdateScoreTextUI(); // Cập nhật giao diện hiển thị điểm
        }
    }

    void Start()
    {
        // Tìm GameObject có tag "ScoreTextTag" để lấy thành phần TextMeshProUGUI
        GameObject scoreGO = GameObject.FindGameObjectWithTag("SocreTextTag");

        if (scoreGO != null)
        {
            // Gán thành phần TextMeshProUGUI để có thể cập nhật nội dung
            scoreTextUI = scoreGO.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            // Nếu không tìm thấy, hiển thị lỗi trong Console
            Debug.LogError("Không tìm thấy GameObject với tag 'SocreTextTag'");
        }

        // Cập nhật giao diện điểm ngay khi bắt đầu
        UpdateScoreTextUI();
    }

    // Hàm cập nhật nội dung hiển thị điểm trên màn hình
    void UpdateScoreTextUI()
    {
        if (scoreTextUI != null)
        {
            // Định dạng điểm thành chuỗi 5 chữ số (ví dụ: 00025)
            string scoreStr = string.Format("{0:00000}", score);
            scoreTextUI.text = scoreStr; // Gán chuỗi vào giao diện
        }
    }
}
