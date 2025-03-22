using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Weapon : MonoBehaviour
{
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    protected ShootAble shooter;
    public abstract void OnHitWith(Entity entity);//override ????????????????? 
    public abstract void Move(); //override ????????????????? 

    public void Init(int newDamage, ShootAble newOwner)
    {
        Damage = newDamage;
        shooter = newOwner;
    }

    private void OnTriggerEnter (Collider other)
    {
        OnHitWith(other.GetComponent<Entity>());
        Destroy(this.gameObject, 5f);
    }
    
}
