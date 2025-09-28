using UnityEngine;

// Script này dùng để hủy (destroy) GameObject mà nó đang gắn vào
public class Destroyer : MonoBehaviour
{
    // Hàm này sẽ hủy GameObject hiện tại khi được gọi
    void DestroyGameObject()
    {
        Destroy(gameObject);// Hủy chính GameObject mà script này đang gắn vào
    }    
}
