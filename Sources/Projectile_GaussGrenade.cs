using UnityEngine;
using Verse;
using System;

public class Projectile_GaussGrenade : Projectile
{
	private int ticksToDetonation;

	public override void ExposeData()
	{
		base.ExposeData();
		Scribe_Values.Look(ref ticksToDetonation, "ticksToDetonation", 0);
	}

	public override void Tick()
	{
		base.Tick();
		if (ticksToDetonation > 0)
		{
			ticksToDetonation--;
			if (ticksToDetonation <= 0)
			{
				Explode();
			}
		}
	}

	public override void Impact(Thing hitThing)
	{
		if (def.projectile.explosionDelay == 0)
		{
			Explode();
			return;
		}
		landed = true;
		ticksToDetonation = def.projectile.explosionDelay;

	}

	protected virtual void Explode()
	{
		Destroy();
		float armorPenetration = -1;
		GenExplosion.DoExplosion(base.Position, base.Map, def.projectile.explosionRadius, def.projectile.damageDef, launcher, -1, armorPenetration, def.projectile.soundExplode, (ThingDef)def, (ThingDef)equipmentDef, null, def.projectile.postExplosionSpawnThingDef, (float)def.projectile.preExplosionSpawnChance, 0, false, null, 0f, 0);
		for (int i = 0; i < 4; i++)
		{
			ThrowSmokeBlue(base.Position.ToVector3Shifted() + Gen.RandomHorizontalVector(def.projectile.explosionRadius * 0.7f), base.Map, def.projectile.explosionRadius * 0.6f);
			ThrowMicroSparksBlue(base.Position.ToVector3Shifted() + Gen.RandomHorizontalVector(def.projectile.explosionRadius * 0.7f), base.Map);
		}
	}

	public static void ThrowSmokeBlue(Vector3 loc, Map map, float size)
	{
		if (loc.ShouldSpawnMotesAt(map) && !map.moteCounter.SaturatedLowPriority)
		{
			MoteThrown obj = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_SmokeBlue"));
			obj.Scale = Rand.Range(1.5f, 2.5f) * size;
			obj.rotationRate = Rand.Range(-30f, 30f);
			obj.exactPosition = loc;
			obj.SetVelocity(Rand.Range(30, 40), Rand.Range(0.5f, 0.7f));
			GenSpawn.Spawn((Thing)obj, loc.ToIntVec3(), map);
		}
	}

	public static void ThrowMicroSparksBlue(Vector3 loc, Map map)
	{
		if (loc.ShouldSpawnMotesAt(map) && !map.moteCounter.SaturatedLowPriority)
		{
			MoteThrown obj = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_MicroSparksBlue"));
			obj.Scale = Rand.Range(0.8f, 1.2f);
			obj.rotationRate = Rand.Range(-12f, 12f);
			obj.exactPosition = loc;
			obj.exactPosition -= new Vector3(0.5f, 0f, 0.5f);
			obj.exactPosition += new Vector3(Rand.Value, 0f, Rand.Value);
			obj.SetVelocity(Rand.Range(35, 45), 1.2f);
			GenSpawn.Spawn((Thing)obj, loc.ToIntVec3(), map);
		}
	}
}
