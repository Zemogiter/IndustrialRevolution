using GaussWeapons;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using static Verse.DamageInfo;

public class Projectile_Gauss : Projectile
{
	public int tickCounter;

	public Thing hitThing;

	public CompExtraDamage compED;

	public override void SpawnSetup(Map map, bool respawningAfterLoad)
	{
		((ThingWithComps)this).SpawnSetup(map, respawningAfterLoad);
		compED = ((ThingWithComps)this).GetComp<CompExtraDamage>();
	}

	public override void Impact(Thing hitThing) 
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		Map map = ((Thing)this).Map;
		((Projectile)this).Impact(hitThing);
		if (hitThing != null)
		{
			int damageAmountBase = ((Thing)this).def.projectile.damageAmountBase;
			ThingDef equipmentDef = base.equipmentDef;
			DamageInfo val = default;
			((DamageInfo)(val)).ctor(((Thing)this).def.projectile.damageDef, damageAmountBase, ((Projectile)this).ExactRotation.eulerAngles.y, base.launcher, (BodyPartRecord)null, equipmentDef, (SourceCategory)0); //to fix: figure out the constructor
			hitThing.TakeDamage(val);
			Pawn val2 = (Pawn)(object)((hitThing is Pawn) ? hitThing : null);
			if (val2 != null && !val2.Downed && Rand.Value < compED.chanceToProc)
			{
				ThrowMicroSparksBlue(base.destination, ((Thing)this).Map); //not sure if ThrowMicroSparksBlue is a correct replacement for MoteMaker.ThrowMicroSparks, need checking
				hitThing.TakeDamage(new DamageInfo(DefDatabase<DamageDef>.GetNamed(compED.damageDef, true), compED.damageAmount, ((Projectile)this).ExactRotation.eulerAngles.y, launcher, (BodyPartRecord)null, (ThingDef)null, (SourceCategory)0)); //to fix: a buch of "cant convert Verse.* to x" errors
			}
		}
		else
		{
			SoundStarter.PlayOneShot(SoundDefOf.BulletImpact_Ground, SoundInfo.InMap(new TargetInfo(((Thing)this).Position, map, false)));
			MoteMaker.MakeStaticMote(((Projectile)this).ExactPosition, map, ThingDefOf.Filth_Dirt, 1f);
			ThrowMicroSparksBlue(((Projectile)this).ExactPosition, ((Thing)this).Map);
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
		if (loc.ShouldSpawnMotesAt(map) && !map.moteCounter.SaturatedLowPriority)
		{
			MoteThrown val = (MoteThrown)ThingMaker.MakeThing(ThingDef.Named("Mote_MicroSparksBlue"), (ThingDef)null);
			((Mote)val).Scale = Rand.Range(0.8f, 1.2f); //to fix: cannot convert this from float to Vector3
			((Mote)val).rotationRate = Rand.Range(-12f, 12f);
			((Mote)val).exactPosition = loc;
			((Mote)val).exactPosition = ((Mote)val).exactPosition - new Vector3(0.5f, 0f, 0.5f);
			((Mote)val).exactPosition = ((Mote)val).exactPosition + new Vector3(Rand.Value, 0f, Rand.Value);
			val.SetVelocity((float)Rand.Range(35, 45), 1.2f);
			GenSpawn.Spawn((Thing)val, IntVec3Utility.ToIntVec3(loc), map);
		}
	}
}
