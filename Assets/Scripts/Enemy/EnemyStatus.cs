using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] float hp = 10f;
    public float HP { get => hp; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void Damage(float damage) 
    {
        if (hp <= 0f) return;
        hp -= damage;
    }
        
}
