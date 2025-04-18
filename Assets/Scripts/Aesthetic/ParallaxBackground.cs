using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]  float SCROLL_WIDTH = 15.3f;
    [SerializeField] private float _scrollSpeed;

    public void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.x -= _scrollSpeed * Time.deltaTime;
        if(transform.position.x <= -SCROLL_WIDTH)
            gameObject.transform.position = new Vector3(SCROLL_WIDTH, transform.position.y, transform.position.z);
        else
            transform.position = position;
    }
}
