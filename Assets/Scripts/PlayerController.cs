using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Color32 playerOriginalColor;
    private Renderer playerRenderer;
    private Color32 rust = new Color32(183, 65, 14, 1);
    [SerializeField] int humanPointValue;
    [SerializeField] int rockPointValue;
    public bool enemyReset = false;
    [SerializeField] float rockPopupForce;
    private float rustyEffectDuration = 5.0f;
    public bool hasRustyEffect = false;
    public bool hasOilEffect = false;
    public bool hasRockSmashEffect = false;
    [SerializeField] float oilEffectDuration;
    [SerializeField] float rockSmashEffectDuration;
    private float turnSpeed = 75;
    public float rollSpeed = 5.0f;
    private float angleLimit = 0.2705979f;
    private float xRotation;
    private float zRotation;
    private float horizontalInput;
    public float resetDistance;
    Color32 colorEnd;
    public GameObject powerupIndicator;
    public Vector3 startPos;

    public ParticleSystem sparkParticle;
    public ParticleSystem smokeParticles;
    private SpawnManager spawnManagerScript;
    private Rigidbody playerRb;
    private GameObject ground;
    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        playerOriginalColor = playerRenderer.material.color;
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        ground = GameObject.Find("Environment/Ground");
        startPos = transform.position;
        colorEnd = rust;
        resetDistance = ground.transform.lossyScale.z * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManagerScript.isGameActive)
        {
            InputHandler();
            TurnLimiter();
            ResetPosition();
        }
    }
    private void LateUpdate()
    {
        if (spawnManagerScript.isGameActive)
        {
            powerupIndicator.transform.position = transform.position;
        }
    }
    private void TurnLimiter()
    {
        if (transform.rotation.y > angleLimit)
        {
            transform.rotation = Quaternion.Euler(0, 45, 90);
        }
        if (transform.rotation.y < -angleLimit)
        {
            transform.rotation = Quaternion.Euler(0, -45, 90);
        }
    }
    private void ResetPosition()
    {
        if (transform.position.z > startPos.z + resetDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
            spawnManagerScript.ResetEnemies();
            spawnManagerScript.ResetPowerups();
        }
    }
    private void InputHandler()
    {
        if (!hasRustyEffect)
        {
            turnSpeed = 75;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        if (transform.position.y < 1)
        {
            if (!hasOilEffect)
            {
                transform.Rotate(Vector3.right * horizontalInput * turnSpeed * Time.deltaTime);
            }
            else if (hasOilEffect)
            {
                float oilEffectMod = Random.Range(-1, 1);
                transform.Rotate(Vector3.right * oilEffectMod * turnSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(turnSpeed);
        }
        transform.Translate(Vector3.forward * rollSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Rusty Puddle(Clone)")
        {
            StartCoroutine(RustyEffectCountdown());
            Destroy(other.gameObject);
            hasRustyEffect = true;
            turnSpeed /= 2;
            playerRenderer.material.color += rust;

        }
        if (other.name == "Rock(Clone)")
        {
            if (!hasRockSmashEffect)
            {
                Instantiate(sparkParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f), sparkParticle.transform.rotation);
                playerRb.AddForce(Vector3.up * rockPopupForce, ForceMode.Impulse);
                Destroy(other.gameObject);
            }
            else
            {
                spawnManagerScript.UpdateScore(rockPointValue);
                Destroy(other.gameObject);
            }
        }
        if (other.name == "Human(Clone)")
        {
            spawnManagerScript.UpdateScore(humanPointValue);
            Destroy(other.gameObject);
        }
        if (other.name == "Oil Slick(Clone)")
        {
            Destroy(other.gameObject);
            playerRenderer.material.color = Color.black;
            hasOilEffect = true;
            StartCoroutine(OilEffectCountdown());

        }
        if (other.CompareTag("Wall"))
        {
            spawnManagerScript.GameOver();
        }
        if (other.CompareTag("Powerup"))
        {
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            hasRockSmashEffect = true;
            StartCoroutine(RockSmashPowerupCountdown());
        }
    }
    IEnumerator RustyEffectCountdown()
    {
        yield return new WaitForSeconds(rustyEffectDuration);
        hasRustyEffect = false;
        playerRenderer.material.color -= rust;
        turnSpeed *= 2;
    }
    IEnumerator OilEffectCountdown()
    {
        yield return new WaitForSeconds(oilEffectDuration);
        playerRenderer.material.color = playerOriginalColor;
        hasOilEffect = false;
    }
    IEnumerator RockSmashPowerupCountdown() {
        yield return new WaitForSeconds(rockSmashEffectDuration);
        powerupIndicator.gameObject.SetActive(false);
        hasRockSmashEffect = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            smokeParticles.Play();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            smokeParticles.Stop();
        }
    }

}
