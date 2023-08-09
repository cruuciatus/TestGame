using Spine.Unity;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public AnimationReferenceAsset angry, run, win, idle;

    [SerializeField] private float _speedEnemy;
    [SerializeField] private ParticleSystem _destroyExsplosion;
   
    private PlayerController _player;    
    private bool _enemyWin = true;
    private SkeletonAnimation _skeletonAnimation;

    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        _player = player.GetComponent<PlayerController>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    public void Start()
    {
        SetAnimation(angry, true);
    }

    public void FixedUpdate()
    { if (_enemyWin == true)
        {
           // SetAnimation(run, true);
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _speedEnemy);
        }
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop)
    {
        _skeletonAnimation.state.SetAnimation(1, animation, loop);
    }
    public void DestroyEnemy()
    {
        Instantiate(_destroyExsplosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }


    public void OnMouseDown()
    {  if (gameObject.tag == "Enemy")
        {
            _player.Shoot();
        }
        
    }

    public void EnemyWin()
    {
        
        _enemyWin = false;
        SetAnimation(win, true);
    }
}
