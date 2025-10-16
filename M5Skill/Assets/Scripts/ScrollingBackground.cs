using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private Renderer backgroundRenderer;
    public float speed = 2.0f;
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
