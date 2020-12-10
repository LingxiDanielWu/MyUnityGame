using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    public float turnSpeed = 1f;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Vector3 _moveDir;
    private Vector3 _oldForward;     //当前摄像机Z轴对准的方向
    private enum MOVE_OPTs{
        Forward = KeyCode.W,
        Back = KeyCode.S,
        Left = KeyCode.A,
        Right = KeyCode.D,
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _oldForward = Camera.main.transform.forward;
        _oldForward.y = 0;
        _moveDir += Input.GetKeyDown((KeyCode)MOVE_OPTs.Forward) ? Vector3.forward : Vector3.zero;
        _moveDir += Input.GetKeyDown((KeyCode)MOVE_OPTs.Back) ? Vector3.back : Vector3.zero;
        _moveDir += Input.GetKeyDown((KeyCode)MOVE_OPTs.Left) ? Vector3.left : Vector3.zero;
        _moveDir += Input.GetKeyDown((KeyCode)MOVE_OPTs.Right) ? Vector3.right : Vector3.zero;
        _moveDir -= Input.GetKeyUp((KeyCode)MOVE_OPTs.Forward) ? Vector3.forward : Vector3.zero;
        _moveDir -= Input.GetKeyUp((KeyCode)MOVE_OPTs.Back) ? Vector3.back: Vector3.zero;
        _moveDir -= Input.GetKeyUp((KeyCode)MOVE_OPTs.Left) ? Vector3.left: Vector3.zero;
        _moveDir -= Input.GetKeyUp((KeyCode)MOVE_OPTs.Right) ? Vector3.right: Vector3.zero;
        
        if (_moveDir != Vector3.zero)
        {
            Quaternion r = Quaternion.RotateTowards(Quaternion.LookRotation(Vector3.forward), Quaternion.LookRotation(_moveDir), 180);
            Vector3 dir = r * _oldForward;
            navMeshAgent.Move(dir.normalized * Time.deltaTime * navMeshAgent.speed);

            Vector3 faceDir = _moveDir.z >= 0 ? dir : _oldForward;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(faceDir), Time.deltaTime * turnSpeed);

            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);

            Vector3 f = transform.forward;
            f.y = 0;
            if (f != _oldForward)
            {
                Quaternion r = Quaternion.LookRotation(_oldForward);
                transform.rotation = Quaternion.Lerp(transform.rotation, r, Time.deltaTime * navMeshAgent.angularSpeed);
            }
            _moveDir = Vector3.zero;
        }
    }
}
