using UnityEngine;

public class ChaseController : MonoBehaviour
{
    private PatrolController ghost;
    public float shootingRate = 1.0f; // Tasa de disparo en segundos
    private float shootCooldown;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public GameObject puntoDisparo;
    public bool canShoot;
    void Start()
    {
        canShoot = true;
        ghost = GetComponentInParent<PatrolController>();
        shootCooldown = 1f;
        projectileSpeed = 200.0f;
    }

    void Update()
    {
        if (ghost.chasing && shootCooldown <= 0f)
        {
            Shoot();
            shootCooldown = shootingRate;
        }

        if (shootCooldown > 0f)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            //Debug.Log("Disparando al jugador");
            // Aquí puedes añadir la lógica para instanciar proyectiles, etc.
            GameObject projectile = Instantiate(projectilePrefab, puntoDisparo.transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            //Vector3 direction = (ghost.player.position - transform.position).normalized;
            rb.AddForce(-transform.up * projectileSpeed);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ghost.chasing = true;
            ghost.agent.destination = ghost.player.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((ghost.transform.position - ghost.player.position).sqrMagnitude > Mathf.Pow(GetComponent<SphereCollider>().radius, 2))
            {
                ghost.chasing = false;
                ghost.GotoNextPoint();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ghost.chasing)
        {
            ghost.agent.destination = ghost.player.position;
        }
    }
}
