using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoAmmoType : MonoBehaviour
{
    // Start is called before the first frame update
    //void InitData(...) {}// TODO(batuhan): Load some data before emitting from tower 
    // to deliver to the target? Damage, Projectile speed, hit effect, etc
    public int Damage { get; private set; }
    public float Speed { get; private set; }

    void Start()
    {
        Speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {            
            Destroy(this);
        }
    }
}
