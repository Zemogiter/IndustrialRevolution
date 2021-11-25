using GaussWeapons;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

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

	protected  void Impact(Thing hitThing)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		Map map = ((Thing)this).get_Map();
		((Projectile)this).Impact(hitThing);
		if (hitThing != null)
		{
			int damageAmountBase = ((Thing)this).def.projectile.damageAmountBase;
			ThingDef equipmentDef = base.equipmentDef;
			DamageInfo val = default;
			((DamageInfo)( val))..ctor(((Thing)this).def.projectile.damageDef, damageAmountBase, ((Projectile)this).get_ExactRotation().eulerAngles.y, base.launcher, (BodyPartRecord)null, equipmentDef, (SourceCategory)0);
			hitThing.TakeDamage(val);
			Pawn val2 = (Pawn)(object)((hitThing is Pawn) ? hitThing : null);
			if (val2 != null && !val2.get_Downed() && Rand.get_Value() < compED.chanceToProc)
			{
				MoteMaker.ThrowMicroSparks(base.destination, ((Thing)this).get_Map());
				hitThing.TakeDamage(new DamageInfo(DefDatabase<DamageDef>.GetNamed(compED.damageDef, true), compED.damageAmount, ((Projectile)this).get_ExactRotation().eulerAngles.y, base.launcher, (BodyPartRecord)null, (ThingDef)null, (SourceCategory)0));
			}
		}
		else
		{
			SoundStarter.PlayOneShot(SoundDefOf.BulletImpactGround, SoundInfo.op_Implicit(new TargetInfo(((Thing)this).get_Position(), map, false)));
			MoteMaker.MakeStaticMote(((Projectile)this).get_ExactPosition(), map, ThingDefOf.Mote_ShotHit_Dirt, 1f);
			ThrowMicroSparksBlue(((Projectile)this).get_ExactPosition(), ((Thing)this).get_Map());
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
