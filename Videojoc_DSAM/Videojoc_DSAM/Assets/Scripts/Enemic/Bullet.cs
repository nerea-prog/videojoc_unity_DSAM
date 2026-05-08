using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir = Vector2.zero;
    public float speed = 5f;
   
    void Update()
    {
        Vector3 dir2 = dir * speed * Time.deltaTime;
        Vector3 currentDir = new Vector3(dir2.x, dir2.y,0);
        transform.position += currentDir;
    }

    private void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag != "bullet")
            {
                Destroy(gameObject);
            }
        }
    }
}
