using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Certifique-se de importar o namespace UI

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    public float        _playerSpeed = 3f;
    public Vector2      _playerDirection;

    private Animator    _playerAnimator;
    private bool        _facingR = true;
    private bool        _isWalk;

    private int         _contaPunch;
    private bool        _canDash = true;
    private float       _dashCooldown = 0.3f;
    private float       _timePunch = 0.75f;

    [SerializeField] float _playerHealth, _maxPlayerHealth = 5f;

    // Referência ao componente Text no Canvas
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = _maxPlayerHealth;
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
       

        UpdateHealthText(); // Atualiza o texto de saúde inicialmente
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateAnimator();

        if((_isWalk == false) && ((Input.GetKeyDown(KeyCode.Z)) || (Input.GetKeyDown(KeyCode.J))))
        {
            StartCoroutine(PunchController());

            if(_contaPunch < 2)
            {
                PlayerJab();
                _contaPunch++;
            }

            else if (_contaPunch >= 2)
            {
                PlayerPunch();
                _contaPunch = 0;
            }

            StopCoroutine(PunchController());
        }       

    }

    void FixedUpdate()
    {
        if((_playerDirection.x != 0)|| (_playerDirection.y != 0))
        {
            _isWalk = true;
        } else {_isWalk = false;}

        _playerRigidBody.MovePosition(_playerRigidBody.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);
    }

    void PlayerMove()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if((_playerDirection.x < 0) && (_facingR == true))
        {
            Flip();
        }

        else if((_playerDirection.x > 0) && (_facingR == false))
        {
            Flip();
        }

        if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K)) && _canDash) // Verifica se o jogador pode realizar um Dash
        {
            StartCoroutine(DashController());
        }
    }

    void UpdateAnimator()
    {
        _playerAnimator.SetBool("isWalk", _isWalk);
    }

    private void Flip()
    {
        _facingR = !_facingR;
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        _playerAnimator.SetTrigger("isJab");
    }
    
    void PlayerPunch()
    {
        _playerAnimator.SetTrigger("isPunch");
    }

    void PlayerDash()
    {
        _playerAnimator.SetTrigger("isDash");
    }

    IEnumerator PunchController()
    {
        yield return new WaitForSeconds(_timePunch);
        _contaPunch = 0;
    }   

    IEnumerator DashController()
    {
        _canDash = false; // Impede que o jogador execute outro Dash enquanto este estiver em andamento
        _playerSpeed *= 3;
        PlayerDash(); // Ativa a animação de Dash
        yield return new WaitForSeconds(_timePunch);
        _playerSpeed /= 3;
        yield return new WaitForSeconds(_dashCooldown); // Aguarda o tempo de recarga do Dash
        _canDash = true; // Permite que o jogador execute outro Dash
    }

    public void PlayerTakeDamage(float damageAmount)
    {
        _playerHealth -= damageAmount;
        Debug.Log("Player health after taking damage: " + _playerHealth);
        UpdateHealthText(); // Atualiza o texto de saúde após tomar dano

        if (_playerHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }

    private void UpdateHealthText()
    {
        Debug.Log("Updating health text to: Health: " + _playerHealth.ToString("F0"));
        healthText.text = "Health: " + _playerHealth.ToString("F0");
    }
}
