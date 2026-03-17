using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortingOrderHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Lower Y = higher sorting order → appears in front
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}