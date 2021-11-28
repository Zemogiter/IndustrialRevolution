using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

public class Verb_ShootJam : Verb_LaunchProjectile
{
	public bool isJammed;

	public SoundDef jamSound = SoundDef.Named("Misfire");

	public override int ShotsPerBurst => verbProps.burstShotCount;

	public override void WarmupComplete()
	{
		if (isJammed)
		{
			if (new System.Random().Next(0, 2) == 1)
			{
				isJammed = false;
				MoteMaker.ThrowText(new Vector3((float)caster.Position.x + 1f, caster.Position.y, (float)caster.Position.z + 1f), caster.Map, "Jam Cleared", Color.white);
				((Verb)this).EquipmentSource.def.soundInteract.PlayOneShot(new TargetInfo(caster.Position, caster.Map));
			}
			return;
		}
		caster.TryGetQuality(out var qc);
		if (Rand.Range(1, qc switch
		{
			QualityCategory.Awful => 30,
			QualityCategory.Normal => 40,
			QualityCategory.Poor => 50,
			QualityCategory.Good => 60,
			QualityCategory.Excellent => 70,
			QualityCategory.Legendary => 80,
			QualityCategory.Masterwork => 90,
			(QualityCategory)7 => 100,
			(QualityCategory)8 => 10000,
			_ => 60,
		}) == 1)
		{
			MoteMaker.ThrowText(new Vector3((float)caster.Position.x + 1f, caster.Position.y, (float)caster.Position.z + 1f), caster.Map, "Jammed", Color.white);
			jamSound.PlayOneShot(new TargetInfo(caster.Position, caster.Map));
			isJammed = true;
			return;
		}
		((Verb)this).WarmupComplete();
		if (base.CasterIsPawn && base.CasterPawn.skills != null)
		{
			float xp = 10f;
			if (currentTarget.Thing != null && currentTarget.Thing.def.category == ThingCategory.Pawn)
			{
				xp = ((!currentTarget.Thing.HostileTo(caster)) ? 50f : 240f);
			}
			base.CasterPawn.skills.Learn(SkillDefOf.Shooting, xp);
		}
	}
}
