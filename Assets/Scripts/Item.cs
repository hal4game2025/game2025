using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�A�C�e���擾");
            Destroy(gameObject); // �A�C�e��������
            
        }
    }
}
