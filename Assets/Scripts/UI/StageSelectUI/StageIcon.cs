using UnityEngine;

public class StageIcon : MonoBehaviour
{
    Animator animator;  // アニメーターコンポーネント

    void Start()
    {
        animator = GetComponent<Animator>();  // アニメーターコンポーネントを取得
    }

    void OnBecameVisible()
    {
        animator.Play("move", 0, 0.0f);
    }

    // ステージアイコンが選択されたときに呼び出されるメソッド
    public void OnSelected()
    {

        SceneManager.Instance.ChangeScene(gameObject.name);
    }
}
