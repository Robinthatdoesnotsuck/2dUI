using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;

    private GameObject _target;
    private Animator _animator;
    private Weapon _weapon;
    
    private void Awake() {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTarget()
    {
        if(_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }

        if (_target.transform.position.x == minX){
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_target.transform.position.x == maxX)
        {
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private IEnumerator PatrolToTarget()
    {
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
        {
            // Update animator
            _animator.SetBool("Idle", false);
            
            // Move to target
            Vector2 direction = _target.transform.position - transform.position;
            float xDirection = direction.x;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            yield return null;
        }
        
        Debug.Log("Target reached");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);
        UpdateTarget();
       
        // Update animator
        _animator.SetBool("Idle", true);

        // Shoot
        _animator.SetTrigger("Shooting");
       
        Debug.Log("waiting for " + waitingTime + " seconds");
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine("PatrolToTarget");
    }
}
