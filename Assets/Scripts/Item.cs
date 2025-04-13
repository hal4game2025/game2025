using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("アイテム取得");
            Destroy(gameObject); // アイテムを消去
            
        }
    }
}
