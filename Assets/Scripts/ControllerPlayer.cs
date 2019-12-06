using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour {

    public float speed;
    public float JumpForce;
    public int MaxJumps;
    public int AvailableJumps;
    private bool isGrounded;

    private Rigidbody rb;
    private int points;
    public Text score;
    public Text victory;
    public float outOutBoundsY;
    private GameObject[] Enemies;
    public Button restartButton;
    private float differenceAngle;
    private float actualYRotation;
    private float desiredAngle;

    public float speedPercent;
    const float animationTransitionTime = 0.1f;
    Animator animator;

    public Image hpBar;
    private float maxHp;
    private float hp;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        points = 0;
        AvailableJumps = MaxJumps;
        isGrounded = true;
        outOutBoundsY = -3f;

        animator = GetComponentInChildren<Animator>();

        maxHp = 150;
        hp = maxHp;
        hpBar.fillAmount = hp / maxHp;
        InvokeRepeating("BloodLoos", 1, 1);
    }

    private void BloodLoos()
    {
        hp -= 5;
        hpBar.fillAmount = hp / maxHp;
    }

    private void Update()
    {
        speedPercent = rb.velocity.magnitude/10;
        if (speedPercent > 1)
        {
            speedPercent = 1;
        }
        animator.SetFloat("speedPercent", speedPercent, animationTransitionTime, Time.deltaTime);
            
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal")*speed;
        float moveVertical = Input.GetAxis("Vertical")*speed;
        Vector3 movimiento = new Vector3(moveHorizontal,0.0f,moveVertical);

        rb.AddForce(movimiento);
        if (Input.GetKeyDown(KeyCode.Space) && AvailableJumps >0)
        {
            animator.SetTrigger("isJumping");
            rb.AddForce(Vector3.up*JumpForce,ForceMode.Impulse);
            AvailableJumps--;
            isGrounded = false;
            ///hp -= 25;
            //hpBar.fillAmount = hp/maxHp;
        }
        outOutBoundsCheck();
        rotatePlayer();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Points"))
        {
            other.gameObject.SetActive(false);
            points++;
            UpdateScore();
            if (points == 10)
            {
                victory.text = "You Won!";
                EndGame();
            }
        }else if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            speed = speed + 2;
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (transform.localScale.x <= 0.4f)
            {
                victory.text = "You Lose!";
                EndGame();
            }
        }else if (other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("Muerte");
            gameObject.SetActive(false);
            victory.text = "You Die!";
            EndGame();
        }
    }

    private void EndGame()
    {
        rb.transform.position = new Vector3(0, 1, 0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = Enemies.Length-1; i >= 0; i--)
        {
            Enemies[i].SetActive(false);
        }
        restartButton.image.gameObject.SetActive(true);
    }

    void UpdateScore()
    {
        score.text = "Score: " + points.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Floor") && isGrounded == false)
        {
            AvailableJumps = MaxJumps;
            isGrounded = true;
        }
    }

    void outOutBoundsCheck()
    {
        if (transform.position.y < outOutBoundsY)
        {
            rb.transform.position = new Vector3(0, 1, 0);
        }
    }

    void rotatePlayer()
    {
        actualYRotation = transform.eulerAngles.y;
        if (actualYRotation > 180.0f)
        {
            actualYRotation -= 360;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            desiredAngle = -90;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            desiredAngle = 90;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            desiredAngle = 180;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            desiredAngle = 0;
        }
        differenceAngle = actualYRotation - desiredAngle;
        if (differenceAngle > 180.0f)
        {
            differenceAngle -= 360;
        }
        else if (differenceAngle < -180.0f)
        {
            differenceAngle += 360;
        }
        if (differenceAngle < 1.0f && differenceAngle > -1.0f)
        {
            rb.angularVelocity = Vector3.zero;
            differenceAngle = 0.0f;
        }
        else if (differenceAngle > 0.0f)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 300);
        }
        else
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 300);
        }

    }
}

