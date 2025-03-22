using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private int health;
    public int Health { get => health; set => health = value; }

    public Rigidbody rb;

    private int score;
    public int Score { get => score; set => score = value; }

    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    public void Init(int newHealth)
    {
        Health = newHealth;
    }

    public float GetHealthPercentage()
    {
        return (float)Health / 100;
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{name} took {damage}, remaining HP: {Health}");

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (IsDead())
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            player.AddScore(GetScoreValue());
        }

        Destroy(gameObject);
    }

    protected virtual int GetScoreValue() { return 0; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}