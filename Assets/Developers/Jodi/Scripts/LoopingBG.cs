using UnityEngine;

public class LoopingBG : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float offset = 0.01f; // Adjustable offset to prevent gaps
    public Transform[] backgrounds; // Assign background objects in the Inspector
    private float spriteWidth; // Width of the sprite

    private void Start()
    {
        // Get sprite width
        spriteWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Move backgrounds
        foreach (Transform bg in backgrounds)
        {
            bg.Translate(Vector2.left * (speed * Time.deltaTime));

            // Check if the background has completely moved out of view
            if (bg.position.x <= -spriteWidth - offset)
            {
                // Find the rightmost background
                Transform rightMost = backgrounds[0];
                foreach (Transform other in backgrounds)
                {
                    if (other.position.x > rightMost.position.x)
                        rightMost = other;
                }

                // Reposition the background to the rightmost position
                bg.position = new Vector3(rightMost.position.x + spriteWidth - offset, bg.position.y, bg.position.z);
            }
        }
    }
}