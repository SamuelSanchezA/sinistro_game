using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int fireRate = 0;
    public int Damage = 25;
    public LayerMask whatToHit;

    //public GameObject bullet;
    public Transform socket;
    public Transform muzzleFlash;
    public Transform hitPrefab;

    // Handle camera shaking
    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    CameraShake cameraShake;
                                

    private float timeToFire = 0f;
    private float timeToSpawnEffect = 0f;

    public float effectSpawnRate = 10f;
    Transform firePoint;

    public Vector2 velocity;

	// Use this for initialization
	void Awake () 
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No firepoint!!!!!!!!");
        }
	}

    private void Start()
    {
        cameraShake = GameMaster.gameMaster.GetComponent<CameraShake>();
        if(cameraShake == null)
        {
            Debug.LogError("No cameraShake script found on GM object");
        }
    }

    // Update is called once per frame
    void Update () 
    {
        if(fireRate == 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Vector2 dir = new Vector2(socket.position.x + 10, socket.position.y);
        Vector2 socketPosition = new Vector2(socket.position.x, socket.position.y);

        if (transform.localScale.x < 0f)
        {
            dir.x = -dir.x;
        }

        RaycastHit2D hit = Physics2D.Raycast(socketPosition, dir - socketPosition, 100, whatToHit);

        if(Time.time >= timeToSpawnEffect)
        {
            Vector3 hitNormal;
            Vector3 hitPos;

            if (hit.collider == null)
            {
                hitNormal = new Vector3(9999, 9999, 9999);
                hitPos = hit.point; 
            }
            else
            {
                hitNormal = hit.normal;
                hitPos = hit.point;
            }
                
            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1/effectSpawnRate;
        }


        if(hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.DamageEnemy(Damage);
            }
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        //GameObject obj = Instantiate(bullet, (Vector2)socket.position, socket.localRotation);
        Transform clone = Instantiate(muzzleFlash, socket.position, socket.rotation) as Transform;
        clone.parent = socket;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.04f);
        //obj.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
        //Destroy(obj, 0.07f); 

        if(hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticle = Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.up, hitNormal));
            Destroy(hitParticle.gameObject, 1f);
        }

         // Shake the camera
        cameraShake.Shake(camShakeAmt, camShakeLength);
    }
}
