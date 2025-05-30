using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CanStreikUI : MonoBehaviour
{
    UIDocument canStreikUIDocument;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] HammerCollision hammerCollision;

    UnityEngine.UIElements.VisualElement visualElement;

    private void Start()
    {
        canStreikUIDocument = GetComponent<UIDocument>();
        visualElement = canStreikUIDocument.rootVisualElement.Q<VisualElement>("CanStreikUI");
    }

    private void Update()
    {
        visualElement.style.display = DisplayStyle.None;
        CollisionType collisionType = hammerCollision.GetCollidingType();
        if (!playerStatus.IsStunned)
        {
            if (collisionType == CollisionType.Enemy || collisionType == CollisionType.Obstacles || playerStatus.CanAirMove)
            {   //show
                visualElement.style.display = DisplayStyle.Flex;
            }
        }
        else
        {
            //hide
            visualElement.style.display = DisplayStyle.None;
        }
    }
}
