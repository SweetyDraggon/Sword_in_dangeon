using System;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
	public List<GameObject> coinPool;

	public List<GameObject> projectilePool;

	public List<GameObject> particlePool;

	public List<GameObject> damageTextPool;

	public GameObject projectilePrefab;

	public GameObject fxParticlePrefab;

	public GameObject damageTextPrefab;

	public GameObject coinPrefab;

	public GameObject treasurePrefab;

	public GameObject healingPotionPrefab;

	public int poolSizeCoins = 50;

	public int poolSizeDamage = 15;

	public int poolSizeProjectiles = 25;

	public int poolSizeParticles = 100;

	public void Start()
	{
		this.coinPool = new List<GameObject>();
		this.particlePool = new List<GameObject>();
		this.projectilePool = new List<GameObject>();
		this.damageTextPool = new List<GameObject>();
		this.prepool();
	}

	public void update(float dt)
	{
		foreach (GameObject current in this.coinPool)
		{
			Coin component = current.GetComponent<Coin>();
			if (current.activeInHierarchy)
			{
				component.update(dt);
			}
		}
		foreach (GameObject current2 in this.particlePool)
		{
			FXParticle component2 = current2.GetComponent<FXParticle>();
			if (current2.active)
			{
				component2.update(dt);
			}
		}
		foreach (GameObject current3 in this.projectilePool)
		{
			Projectile component3 = current3.GetComponent<Projectile>();
			if (current3.active)
			{
				component3.update(dt);
			}
		}
		foreach (GameObject current4 in this.damageTextPool)
		{
			DamageText component4 = current4.GetComponent<DamageText>();
			if (current4.active)
			{
				component4.update(dt);
			}
		}
	}

	public void prepool()
	{
		for (int i = 0; i < this.poolSizeCoins; i++)
		{
			this.getCoin();
		}
		for (int j = 0; j < this.poolSizeDamage; j++)
		{
			this.getDamageText();
		}
		for (int k = 0; k < this.poolSizeProjectiles; k++)
		{
			this.getProjectile(ProjectileType.ARROW);
		}
		for (int l = 0; l < this.poolSizeParticles; l++)
		{
			this.getParticle(FXParticleTypes.WOOD);
		}
		this.reset();
	}

	public void reset()
	{
		foreach (GameObject current in this.coinPool)
		{
			current.SetActive(false);
		}
		foreach (GameObject current2 in this.damageTextPool)
		{
			current2.SetActive(false);
		}
		foreach (GameObject current3 in this.projectilePool)
		{
			current3.SetActive(false);
		}
		foreach (GameObject current4 in this.particlePool)
		{
			current4.SetActive(false);
		}
	}

	public DamageText getDamageText()
	{
		DamageText damageText = null;
		foreach (GameObject current in this.damageTextPool)
		{
			if (!current.active)
			{
				damageText = current.GetComponent<DamageText>();
				if (damageText != null)
				{
					return damageText;
				}
			}
		}
		if (damageText == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.damageTextPrefab) as GameObject;
			damageText = gameObject.GetComponent<DamageText>();
			damageText.init();
			damageText.transform.parent = base.transform;
			this.damageTextPool.Add(gameObject);
		}
		return damageText;
	}

	public void emitDamageText(Vector2 pos, int damage, bool critical = false)
	{
		DamageText damageText = this.getDamageText();
		damageText.reset();
		damageText.SetPosition(pos.x, pos.y);
		damageText.setDamage(damage, critical);
		damageText.gameObject.SetActive(true);
	}

	public void emitCustomText(Vector2 pos, string text)
	{
		DamageText damageText = this.getDamageText();
		damageText.reset();
		damageText.SetPosition(pos.x, pos.y);
		damageText.setText(text);
		damageText.gameObject.SetActive(true);
	}

	public Projectile getProjectile(ProjectileType type = ProjectileType.ARROW)
	{
		Projectile projectile = null;
		foreach (GameObject current in this.projectilePool)
		{
			if (!current.active)
			{
				if (type == ProjectileType.ARROW && current.GetComponent<Arrow>() != null)
				{
					Projectile component = current.GetComponent<Arrow>();
					return component;
				}
				if (type == ProjectileType.ENERGY_ORB && current.GetComponent<EnergyOrb>() != null)
				{
					Projectile component = current.GetComponent<EnergyOrb>();
					return component;
				}
				if (type == ProjectileType.ENERGY_ORB2 && current.GetComponent<EnergyOrb2>() != null)
				{
					Projectile component = current.GetComponent<EnergyOrb2>();
					return component;
				}
				if (type == ProjectileType.SPEAR && current.GetComponent<Spear>() != null)
				{
					Projectile component = current.GetComponent<Spear>();
					return component;
				}
				if (type == ProjectileType.BUTCHER_AXE && current.GetComponent<ButcherAxe>() != null)
				{
					Projectile component = current.GetComponent<ButcherAxe>();
					return component;
				}
				if (type == ProjectileType.THROWING_AXE && current.GetComponent<ThrowingAxe>() != null)
				{
					Projectile component = current.GetComponent<ThrowingAxe>();
					return component;
				}
				if (type == ProjectileType.FIREBALL && current.GetComponent<Fireball>() != null)
				{
					Projectile component = current.GetComponent<Fireball>();
					return component;
				}
				if (type == ProjectileType.CANNONBALL && current.GetComponent<CannonBall>() != null)
				{
					Projectile component = current.GetComponent<CannonBall>();
					return component;
				}
				if (type == ProjectileType.ACID_BOLT && current.GetComponent<AcidBolt>() != null)
				{
					Projectile component = current.GetComponent<AcidBolt>();
					return component;
				}
			}
		}
		if (projectile == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.projectilePrefab) as GameObject;
			if (type == ProjectileType.ARROW)
			{
				projectile = gameObject.AddComponent<Arrow>();
			}
			else if (type == ProjectileType.ENERGY_ORB)
			{
				projectile = gameObject.AddComponent<EnergyOrb>();
			}
			else if (type == ProjectileType.ENERGY_ORB2)
			{
				projectile = gameObject.AddComponent<EnergyOrb2>();
			}
			else if (type == ProjectileType.SPEAR)
			{
				projectile = gameObject.AddComponent<Spear>();
			}
			else if (type == ProjectileType.BUTCHER_AXE)
			{
				projectile = gameObject.AddComponent<ButcherAxe>();
			}
			else if (type == ProjectileType.THROWING_AXE)
			{
				projectile = gameObject.AddComponent<ThrowingAxe>();
			}
			else if (type == ProjectileType.FIREBALL)
			{
				projectile = gameObject.AddComponent<Fireball>();
			}
			else if (type == ProjectileType.CANNONBALL)
			{
				projectile = gameObject.AddComponent<CannonBall>();
			}
			else if (type == ProjectileType.ACID_BOLT)
			{
				projectile = gameObject.AddComponent<AcidBolt>();
			}
			else
			{
				projectile = gameObject.AddComponent<Projectile>();
			}
			projectile.init();
			projectile.transform.parent = base.transform;
			this.projectilePool.Add(gameObject);
		}
		return projectile;
	}

	public void emitProjectile(Vector2 pos, ProjectileType type = ProjectileType.ARROW, int scaleX = 1, int direction = 1, int damage = 0)
	{
		Projectile projectile = this.getProjectile(type);
		projectile.reset();
		projectile.scaleX = ((scaleX >= 0) ? 1f : -1f);
		projectile.SetPosition(pos.x, pos.y);
		if (damage != 0)
		{
			projectile.damage = damage;
		}
		if (type == ProjectileType.ARROW)
		{
			projectile.SetPosition(pos.x + projectile.scaleX * 26f, pos.y - 2f);
		}
		else if (type == ProjectileType.ENERGY_ORB)
		{
			EnergyOrb component = projectile.GetComponent<EnergyOrb>();
			component.type = direction;
			component.updateVelocity();
		}
		else if (type == ProjectileType.ENERGY_ORB2)
		{
			EnergyOrb2 component2 = projectile.GetComponent<EnergyOrb2>();
			component2.type = direction;
			component2.updateVelocity();
		}
		else if (type == ProjectileType.SPEAR)
		{
			projectile.SetPosition(pos.x + projectile.scaleX * 26f, pos.y);
		}
		else if (type != ProjectileType.FIREBALL)
		{
			if (type != ProjectileType.CANNONBALL)
			{
				if (type == ProjectileType.BUTCHER_AXE)
				{
					projectile.SetPosition(pos.x, pos.y);
					ButcherAxe component3 = projectile.GetComponent<ButcherAxe>();
					component3.type = 1;
					component3.updateVelocity();
				}
				else if (type == ProjectileType.THROWING_AXE)
				{
					projectile.SetPosition(pos.x, pos.y);
					ThrowingAxe component4 = projectile.GetComponent<ThrowingAxe>();
					component4.type = direction;
					component4.updateVelocity();
				}
			}
		}
		projectile.gameObject.SetActive(true);
	}

	public Coin getCoin()
	{
		Coin coin = null;
		foreach (GameObject current in this.coinPool)
		{
			if (!current.active)
			{
				return current.GetComponent<Coin>();
			}
		}
		if (coin == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.coinPrefab) as GameObject;
			gameObject.name = "Coin";
			coin = gameObject.GetComponent<Coin>();
			coin.transform.parent = base.transform;
			this.coinPool.Add(gameObject);
		}
		return coin;
	}

	public void emitCoins(Vector2 pos, float direction = 1f, int amt = 1)
	{
		if (GameCore.Instance.ownsCoinDoubler)
		{
			amt *= 2;
		}
		int i = amt;
		int num = 0;
		int num2 = i;
		int num3 = 3;
		int num4 = 5;
		i = num2;
		while (i > 0)
		{
			int t;
			if (i - 3 >= num4)
			{
				t = 3;
				i -= 3;
			}
			else if (i - 2 >= num3)
			{
				t = 2;
				i -= 2;
			}
			else
			{
				t = 1;
				i--;
			}
			num++;
			Coin coin = this.getCoin();
			coin.initWithType(t);
			coin.SetPosition(pos.x, pos.y);
			coin.gameObject.SetActive(true);
		}
	}

	public void emitHealingPotion(Vector2 pos, float direction = 1f)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.healingPotionPrefab) as GameObject;
		gameObject.name = "Potion";
		HealingPotion component = gameObject.GetComponent<HealingPotion>();
		component.transform.parent = Game.Instance.map.pickupsGO.transform;
		component.init();
		component.SetPosition(pos.x, pos.y);
		Game.Instance.map.pickups.Add(gameObject);
	}

	public void emitTreasure(Vector2 pos, float direction = 1f, int amt = 1)
	{
		for (int i = 0; i < amt; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.treasurePrefab) as GameObject;
			gameObject.name = "Treasure";
			Treasure component = gameObject.GetComponent<Treasure>();
			component.transform.parent = Game.Instance.map.pickupsGO.transform;
			component.init();
			component.SetPosition(pos.x, pos.y);
			Game.Instance.map.pickups.Add(gameObject);
		}
	}

	public FXParticle getParticle(FXParticleTypes type = FXParticleTypes.WOOD)
	{
		FXParticle fXParticle = null;
		foreach (GameObject current in this.particlePool)
		{
			if (!current.active)
			{
				return current.GetComponent<FXParticle>();
			}
		}
		if (fXParticle == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.fxParticlePrefab) as GameObject;
			fXParticle = gameObject.GetComponent<FXParticle>();
			fXParticle.init();
			fXParticle.transform.parent = base.transform;
			this.particlePool.Add(gameObject);
		}
		return fXParticle;
	}

	public void emitParticles(Vector2 pos, float direction = 1f, FXParticleTypes type = FXParticleTypes.WOOD, int amt = 1)
	{
		for (int i = 0; i < amt; i++)
		{
			FXParticle particle = this.getParticle(type);
			particle.reset();
			particle.SetPosition(pos.x, pos.y);
			particle.SetType(type);
			particle.gameObject.SetActive(true);
		}
	}

	public void emitHead(Vector2 pos, float direction, int headNum)
	{
		FXParticle particle = this.getParticle(FXParticleTypes.HEAD);
		particle.reset();
		particle.SetPosition(pos.x, pos.y);
		particle.SetType(FXParticleTypes.HEAD);
		particle.yVel = UnityEngine.Random.Range(5f, 10f);
		particle.xVel = UnityEngine.Random.Range(0f, 8f) * -direction;
		particle.frame = headNum;
		particle.gameObject.SetActive(true);
	}

	public void emitFlash(Vector2 pos, FXParticleTypes type = FXParticleTypes.FLASH_SMALL)
	{
		FXParticle particle = this.getParticle(type);
		particle.reset();
		particle.SetPosition(pos.x, pos.y);
		particle.SetType(type);
		particle.makeStationary();
		particle.frame = 1;
		if (type == FXParticleTypes.FLASH_COIN_SPARK)
		{
			particle.numFrames = 12;
		}
		else if (type == FXParticleTypes.FLASH_LEVEL_UP)
		{
			particle.numFrames = 14;
		}
		else if (type == FXParticleTypes.FLASH_HIT)
		{
			particle.frame = 3;
			particle.numFrames = 8;
		}
		else if (type == FXParticleTypes.FLASH_SMALL)
		{
			particle.fadeOutSpeed = 0.1f;
			particle.fadeOutCutoff = 0.2f;
		}
		else if (type == FXParticleTypes.EXPLOSION_SMALL)
		{
			particle.numFrames = 12;
		}
		particle.gameObject.SetActive(true);
	}

	public void debugDraw()
	{
		this.updateDebugArray(this.projectilePool);
	}

	public void updateDebugArray(List<GameObject> objects)
	{
		foreach (GameObject current in objects)
		{
			Entity component = current.GetComponent<Entity>();
			if (component && component.alive)
			{
				Debugger.Instance.DrawRect(component.collisionRect);
			}
		}
	}
}
