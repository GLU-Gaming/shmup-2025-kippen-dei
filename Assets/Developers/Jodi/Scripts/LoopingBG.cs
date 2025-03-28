using UnityEngine;

public class LoopingBG : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float offset = 1f; // Small offset to prevent gaps
    public Transform[] backgrounds; // Assign background objects in the Inspector
    private float spriteWidth; // Width of the sprite

    private void Start()
    {
        spriteWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.Translate(Vector2.left * (speed * Time.deltaTime));
        }
        
        foreach (Transform bg in backgrounds)
        {
            float loopThreshold = -spriteWidth - offset;

            if (bg.position.x <= loopThreshold)
            {
                // Find the current rightmost background
                Transform rightMost = backgrounds[0];
                foreach (Transform other in backgrounds)
                {
                    if (other.position.x > rightMost.position.x)
                        rightMost = other;
                }

                // Reposition to the right of the rightmost background
                Vector3 newPosition = new Vector3(rightMost.position.x + spriteWidth, bg.position.y, bg.position.z);
                bg.position = newPosition;
            }
        }
    }
}