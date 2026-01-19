using UnityEngine;

public class EarthMoon : MonoBehaviour
{
    [SerializeField] float timeSpeed = 1f;
    [SerializeField] GameObject Earth;
    [SerializeField] GameObject Moon;

    Vector3 velocityMoon;
    Vector3 accelerationMoon;
    Vector3 difference;
    Vector3 direction;

    float distance;

    void Start()
    {
        velocityMoon = new Vector3(0.5f, 0, 3);
    }

    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Earth.transform.Rotate(0, -1f, 0);
        Moon.transform.LookAt(Earth.transform);

        difference = Earth.transform.position - Moon.transform.position;
        distance = difference.magnitude;
        direction = difference.normalized;

        // F = G * M1 * M2 / (r^2)
        accelerationMoon = 300 * direction / (distance * distance);

        // kinnetic loop
        velocityMoon += accelerationMoon * Time.deltaTime * timeSpeed;
        Moon.transform.position += velocityMoon * Time.deltaTime * timeSpeed;
    }
}
