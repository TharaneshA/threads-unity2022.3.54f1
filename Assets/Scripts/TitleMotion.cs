using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    public float speed = 2f;      // Speed of the animation
    public float height = 20f;    // Height of the up-and-down motion

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        float newY = Mathf.Lerp(startPosition.y - height, startPosition.y + height, pingPong);
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

