using UnityEngine;

public class BadCubeMovement : MonoBehaviour
{
    public float speed = 5f;
    public float radius = 10f;
    private float angle = 0f;
    
    private float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0f, Mathf.PI * 2);
        transform.position = new Vector3(
            Random.Range(-20f, 20f), 
            Random.Range(-20f, 20f), 
            Random.Range(-20f, 20f)
        );
    }

    void Update()
    {
        angle += Time.deltaTime * speed;
        
        Vector3 newPos = transform.position;
        newPos.x += Mathf.Sin(angle + randomOffset) * Time.deltaTime * speed;
        newPos.y += Mathf.Cos(angle + randomOffset) * Time.deltaTime * speed;
        
        if (newPos.x > 50f) newPos.x = -50f;
        if (newPos.x < -50f) newPos.x = 50f;
        if (newPos.y > 50f) newPos.y = -50f;
        if (newPos.y < -50f) newPos.y = 50f;

        transform.position = newPos;
        
        transform.Rotate(Vector3.up, speed * 50 * Time.deltaTime);
    }
}