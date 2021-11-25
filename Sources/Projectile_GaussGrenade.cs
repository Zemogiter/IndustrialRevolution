using UnityEngine;
using Verse;

public class Projectile_GaussGrenade : Projectile
{
	private int ticksToDetonation;

	public override void ExposeData()
	{
		((Projectile)this).ExposeData();
		Scribe_Values.Look<int>(ref ticksToDetonation, "ticksToDetonation", 0, false);
	}

	public override void Tick()
	{
		((Projectile)this).Tick();
		if (ticksToDetonation > 0)
		{
			ticksToDetonation--;
			if (ticksToDetonation <= 0)
			{
				Explode();
			}
		}
	}

	protected  void Impact(Thing hitThing)
	{
		if (((Thing)this).def.projectile.explosionDelay == 0)
		{
			Explode();
			return;
		}
		base.landed = true;
		ticksToDetonation = ((Thing)this).def.projectile.explosionDelay;
	}

	protected virtual void Explode()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		((Thing)this).Destroy((DestroyMode)0);
		GenExplosion.DoExplosion(((Thing)this).get_Position(), ((Thing)this).get_Map(), ((Thing)this).def.projectile.explosionRadius, ((Thing)this).def.projectile.damageDef, base.launcher, ((Thing)this).def.projectile.soundExplode, ((Thing)this).def, base.equipmentDef, ((Thing)this).def.projectile.postExplosionSpawnThingDef, ((Thing)this).def.projectile.explosionSpawnChance, 0, false, (ThingDef)null, 0f, 0);
		for (int i = 0; i < 4; i++)
		{
			IntVec3 position = ((Thing)this).get_Position();
			ThrowSmokeBlue(((IntVec3)( position)).ToVector3Shifted() + Gen.RandomHorizontalVector(((Thing)this).def.projectile.explosionRadius * 0.7f), ((Thing)this).get_Map(), ((Thing)this).def.projectile.explosionRadius * 0.6f);
			position = ((Thing)this).get_Position();
			ThrowMicroSparksBlue(((IntVec3)( position)).ToVector3Shifted() + Gen.RandomHorizontalVector(((Thing)this).def.projectile.explosionRadius * 0.7f), ((Thing)this).get_Map());
		}
	}

	public static void ThrowSmokeBlue(Vector3 loc, Map map, float size)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		if (GenView.ShouldSpawnMotesAt(loc, map) && !map.moteCounter.get_SaturatedLowPriority())
		{
			MoteThrown val = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_SmokeBlue"), (ThingDef)null);
			((Mote)val).set_Scale(Rand.Range(1.5f, 2.5f) * size);
			((Mote)val).rotationRate = Rand.Range(-30f, 30f);
			((Mote)val).exactPosition = loc;
			val.SetVelocity((float)Rand.Range(30, 40), Rand.Range(0.5f, 0.7f));
			GenSpawn.Spawn((Thing)val, IntVec3Utility.ToIntVec3(loc), map);
		}
	}

	public static void ThrowMicroSparksBlue(Vector3 loc, Map map)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		if (GenView.ShouldSpawnMotesAt(loc, map) && !map.moteCounter.get_SaturatedLowPriority())
		{
			MoteThrown val = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_MicroSparksBlue"), (ThingDef)null);
			((Mote)val).set_Scale(Rand.Range(0.8f, 1.2f));
			((Mote)val).rotationRate = Rand.Range(-12f, 12f);
			((Mote)val).exactPosition = loc;
			((Mote)val).exactPosition = ((Mote)val).exactPosition - new Vector3(0.5f, 0f, 0.5f);
			((Mote)val).exactPosition = ((Mote)val).exactPosition + new Vector3(Rand.get_Value(), 0f, Rand.get_Value());
			val.SetVelocity((float)Rand.Range(35, 45), 1.2f);
			GenSpawn.Spawn((Thing)val, IntVec3Utility.ToIntVec3(loc), map);
		}
	}
}
