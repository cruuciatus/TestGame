
using UnityEngine;



public class SpawnEnemy : MonoBehaviour
{
    public GameObject _enemy;
    [SerializeField] private float posX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(_enemy, new Vector3(this.transform.position.x + posX, this.transform.position.y, transform.position.z), transform.rotation);
        }

   }    
}
