using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;


public class HealthComponent : MonoBehaviour, IDamageable, IHealable, IRecieveBonusHearts
{
    public UnityEvent OnDeath;
    public int initialHealth;

    private HealthSystem healthSystem;
    public HealthSystem HealthSystem
    {
        get
        {
            if (healthSystem != null)
            {
                return healthSystem;
            }
            
            healthSystem = new HealthSystem(initialHealth);
            return healthSystem;
        }
    }
    
    public void Heal(int healing) => HealthSystem.Heal(healing);

    public void FullHeal() => HealthSystem.FullHeal();
    public void Damage(int damage) => HealthSystem.Damage(damage);
    
    public void RecieveBonusHearts(int hearts) => HealthSystem.AddTempHearts(hearts);
    
    private void Die()
    {
        OnDeath.Invoke();
    }
}

public interface IRecieveBonusHearts
{
    void RecieveBonusHearts(int hearts);
}

public interface IDamageable
{
    void Damage(int damage);
}

public interface IHealable
{
    void Heal(int healing);

    void FullHeal();
}


// Here is the Logic that we can Unit Test
[System.Serializable]
public class HealthSystem
{
    
    public HealthSystem(int _maxHealth)
    {
        MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
    }

    public event EventHandler<HealthChangedArgs> HealthChanged = delegate { };
    public event EventHandler Died = delegate {  };
    
    public int CurrentHealth { get; private set; } = 0;
    public int MaxHealth { get; private set; } = 0;

    public int BonusHearts { get; private set; } = 0;

    public bool IsDead { get; private set; } = false;

    private void SignalHealthChanged()
    {
        HealthChanged(this, new HealthChangedArgs(CurrentHealth, MaxHealth, BonusHearts));
    }
    

    public void Damage(int damage)
    {
        // Error, 3 Bonus Hearts, then taking 2 damages lead to one bonus and one regular heart
        int piercingDamage = damage - BonusHearts;

        BonusHearts = Mathf.Max(0, BonusHearts - damage);

        piercingDamage = Mathf.Max(0, piercingDamage);

        CurrentHealth = Mathf.Max(0, CurrentHealth - piercingDamage);
        
        SignalHealthChanged();

        if (CurrentHealth == 0)
        {
            IsDead = true;
        }
    }

    public void AddTempHearts(int heartsToAdd)
    {
        BonusHearts += heartsToAdd;
        SignalHealthChanged();
    }

    public void FullHeal()
    {
        CurrentHealth = MaxHealth;
        
        SignalHealthChanged();
    }

    public void Heal(int healing)
    {

        if (IsDead)
        {
            return;
        }

        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + healing);
        
        SignalHealthChanged();
    }
}

public class HealthChangedArgs : EventArgs
{
    public int Health { get; }
    public int MaxHealth { get; }
    public int TempHealth { get; }

    public HealthChangedArgs(int health, int maxHealth, int tempHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
        TempHealth = tempHealth;
    }
    
}
