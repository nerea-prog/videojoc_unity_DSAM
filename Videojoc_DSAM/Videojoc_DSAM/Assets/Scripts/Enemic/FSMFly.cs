using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class FSMFly : MonoBehaviour
{
    enum EFly
    {
        Move,
        Attack
    }


    private Rigidbody2D _rigidbody;
    public float speed = 3.0f;
    [SerializeField]
    Vector2 direction = new Vector2(1, 0.25f);

    [SerializeField] LayerMask Ground;
    public Transform UpPoint;
    public Transform LateralPoint;
    public Transform DownPoint;
    bool upHit, lateralHit, downHit;

    public Transform Player;
    float attackDistance = 10.0f;
    float timerToShoot;
    public float radiusDetectWalls = 0.25f;


    FSM<EFly> brain;
    public Bullet bullet;
    float _elapsedTime = 0f;
    float _ratioShoot = 0.2f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        initFMS();
    }

    private void Update()
    {
        brain.Update();
    }

    public void initFMS()
    {
        brain = new FSM<EFly>(EFly.Move);
        brain.SetOnEnter(EFly.Move, () => timerToShoot = 0f);
        brain.SetOnEnter(EFly.Attack, () => { });
        brain.SetOnStay(EFly.Move, MoveUpdate);
        brain.SetOnStay(EFly.Attack, AttackUpdate);
    }
    public void MoveUpdate()
    {
        _rigidbody.linearVelocity = direction * speed;

        if (DetectColision())
        {
            ChangeDirection();
        }

        if (isPlayerCloseByDistance())
        {
            brain.ChangeState(EFly.Attack);
        }
    }
    public void AttackUpdate()
    {
        direction = Player.position - transform.position;
        direction.Normalize();
        _rigidbody.linearVelocity = direction * speed;
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > _ratioShoot)
        {
            _elapsedTime = 0f;
            Bullet currentBullet = Instantiate(bullet,transform.position,Quaternion.identity);
            currentBullet.dir = direction;
        }
    }
    bool DetectColision()
    {
        upHit = Physics2D.OverlapCircle(UpPoint.transform.position, radiusDetectWalls, Ground);

        lateralHit = Physics2D.OverlapCircle(LateralPoint.transform.position, radiusDetectWalls, Ground);

        downHit = Physics2D.OverlapCircle(DownPoint.transform.position, radiusDetectWalls, Ground);


        return upHit || lateralHit || downHit;
    }

    void ChangeDirection()
    {
        if (lateralHit)
        {
            transform.Rotate(0, 180, 0);
            direction.x = -direction.x;
        }

        if (upHit && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        if (downHit && direction.y < 0)
        {
            direction.y = -direction.y;
        }
    }

    bool isPlayerCloseByDistance()
    {
        if(Vector2.Distance(transform.position, Player.position) <= attackDistance)
        {
            return true;
        }
        return false;
    }
}
