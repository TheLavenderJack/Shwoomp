using UnityEngine;

public class FogBubble : MonoBehaviour
{
    [Header("Motion Settings")]
    public float verticalSpeed = 0.5f;
    public float horizontalFrequency = 2f;
    public Vector2 horizontalAmplitudeRange = new Vector2(0.3f, 0.7f);

    [Header("Size Settings")]
    public Vector2 startRadiusRange = new Vector2(0.6f, 1.2f);
    public float shrinkSpeed = 0.2f;

    private float elapsedTime = 0f;
    private SpriteMask mask;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private float horizontalAmplitude;
    private float startRadius;
    private float phaseOffset;

    void Start()
    {
        mask = GetComponent<SpriteMask>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position;
        horizontalAmplitude = Random.Range(horizontalAmplitudeRange.x, horizontalAmplitudeRange.y);
        startRadius = Random.Range(startRadiusRange.x, startRadiusRange.y);
        phaseOffset = Random.Range(0f, 2f * Mathf.PI); // Full sine wave cycle

        // Apply initial scale
        mask.transform.localScale = new Vector3(startRadius, startRadius, 1f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Move upward and sway
        float newX = startPosition.x + Mathf.Sin(elapsedTime * horizontalFrequency + phaseOffset) * horizontalAmplitude;
        float newY = startPosition.y + verticalSpeed * elapsedTime;
        transform.position = new Vector3(newX, newY, transform.position.z);

        // Shrink the mask
        float currentScale = mask.transform.localScale.x;
        float newScale = currentScale - shrinkSpeed * Time.deltaTime;
        newScale = Mathf.Max(0f, newScale);
        mask.transform.localScale = new Vector3(newScale, newScale, 1f);

        // Fade out (alpha proportional to scale)
        if (spriteRenderer != null)
        {
            float alpha = Mathf.InverseLerp(0f, startRadius, newScale);
            Color color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
        }

        // Destroy when almost invisible/small
        if (newScale < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}