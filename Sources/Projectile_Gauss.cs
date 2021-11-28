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
		base.SpawnSetup(map, respawningAfterLoad);
		compED = GetComp<CompExtraDamage>();
	}

	public override void Impact(Thing hitThing)
	{
		Map map = base.Map;
		base.Impact(hitThing);
		if (hitThing != null)
		{
			int damageAmountBase = def.projectile.damageAmountBase;
			ThingDef thingDef = equipmentDef;
			DamageInfo damageInfo = new(def.projectile.damageDef, damageAmountBase, 0, ExactRotation.eulerAngles.y, launcher, null, equipmentDef, 0);
			hitThing.TakeDamage(damageInfo);
			if (hitThing is Pawn pawn && !pawn.Downed && Rand.Value < compED.chanceToProc)
			{
				//MoteMaker.ThrowMicroSparks(destination, base.Map); since MoteMaker no longer has a definition for ThrowMicroSparks I decided to use ThrowMicroSparksBlue
				ThrowMicroSparksBlue(destination, base.Map);
				hitThing.TakeDamage(new DamageInfo(DefDatabase<DamageDef>.GetNamed(compED.damageDef), compED.damageAmount, 0, ExactRotation.eulerAngles.y, launcher, null, null, SourceCategory.ThingOrUnknown));
			}
		}
		else
		{
			SoundDefOf.BulletImpact_Ground.PlayOneShot(new TargetInfo(base.Position, map));
			MoteMaker.MakeStaticMote(ExactPosition, map, ThingDefOf.Filth_Dirt, 1f);
			ThrowMicroSparksBlue(ExactPosition, base.Map);
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
