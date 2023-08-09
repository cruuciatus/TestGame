using UnityEngine;
using Spine.Unity;
using Spine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimationReferenceAsset walk, run, shoot, idle, loose, shoot_fail;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioSource _audioShoot;
  
    [SerializeField] private float _speed;

    private SkeletonAnimation _skeletonAnimation;
    private EnemyMovement _enemy;
    private SpawnEnemy _spawnEnemy;
    private bool _isAlive = true;

    private void Start()
    {
       
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _skeletonAnimation.state.Event += OnEvent;
        SetAnimation(run, true);


    }

    private void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "shooter/fire")
        {
            _explosion.transform.position = _shootPoint.transform.position;
            _explosion.Play();
            _enemy.DestroyEnemy();
           
        }
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop)
    {
        _skeletonAnimation.state.SetAnimation(1, animation, loop);
    }
   
    private void FixedUpdate()
    {
        if (_isAlive == true)
        _rigidbody.position = new Vector2(_rigidbody.position.x + _speed, _rigidbody.position.y);
    }

   
    public void Shoot()
    {
        if (_cooldown.CanDoAction && _isAlive == true)
        {
            _audioShoot.Play();
            _isAlive = false;
            EnemyMovement enemy = FindObjectOfType<EnemyMovement>();
            _enemy = enemy.GetComponent<EnemyMovement>();
            SetAnimation(shoot, false);
             _cooldown.StartTimer();
            Invoke(nameof(ChangeBool), 2f);
        }
       
    }


    private void ChangeBool()
    {
        SetAnimation(run, true);
        _isAlive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Enemy")
        {
             EnemyMovement enemy = FindObjectOfType<EnemyMovement>();
            _enemy = enemy.GetComponent<EnemyMovement>();
            _enemy.EnemyWin();
            
            SetAnimation(loose, false);
            _gameManager.ShowLooseCanvas();
            _isAlive = false;
        }
        else  if (collision.gameObject.tag == "WinObject")
        {
            _isAlive = false;
            SetAnimation(idle, true);
            _gameManager.ShowWinCanvas();
        }

    }
}
