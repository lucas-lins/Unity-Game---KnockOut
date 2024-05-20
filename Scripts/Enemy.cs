using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float        _enemySpeed = 2f;
    public float        _attackRange = 1.5f;
    public int          _damage = 1;
    private bool        _isEnemyWalk;

    private Transform   _target;
    private Rigidbody2D _enemyRB;
    private Animator    _enemyAnimator;
    Vector2 _moveDirection;
    private bool        _isAttacking = false;
    private EnemyRespawn _enemyRespawn;

    public static event Action<Enemy> _onEnemyKilled;
    [SerializeField] float _health, _maxHealth = 3f;

    private void Awake()
    {
        _enemyRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyAnimator = GetComponent<Animator>();
        _enemyRespawn = GetComponent<EnemyRespawn>();
    }

    void UpdateEnemyAnimator()
    {
        _enemyAnimator.SetBool("isEnemyWalk", _isEnemyWalk);
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            // Movimento em direção ao jogador
            Vector2 direction = (_target.position - transform.position).normalized;
            _moveDirection = direction;

            // Atualiza _isEnemyWalk com base no movimento
            _isEnemyWalk = direction.magnitude > 0.1f; // Considera movimento quando a magnitude for maior que um pequeno valor

            // Verifica se o jogador está dentro do alcance de ataque
            if (Vector2.Distance(transform.position, _target.position) <= _attackRange)
            {
                // Executa a animação de ataque repetidamente
                if (!_isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }

            // Atualiza o Animator
            UpdateEnemyAnimator();
        }
    }

    void FixedUpdate()
    {
        if (_target)
        {
            _enemyRB.velocity = _moveDirection * _enemySpeed;
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        while (Vector2.Distance(transform.position, _target.position) <= _attackRange)
        {
            // Executa a animação de ataque
            _enemyAnimator.SetTrigger("isEnemyAttack");
            yield return new WaitForSeconds(1f); // Tempo entre ataques, ajuste conforme necessário
        }
        _isAttacking = false;
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            Destroy(gameObject);
            _enemyRespawn.StartRespawnTimer();
        }

    }
}