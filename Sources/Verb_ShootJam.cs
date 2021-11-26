using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

public class Verb_ShootJam : Verb_LaunchProjectile
{
	public bool isJammed;

	public SoundDef jamSound = SoundDef.Named("Misfire");

	public override int ShotsPerBurst => ((Verb)this).verbProps.burstShotCount;

	public override void WarmupComplete()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected I4, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Invalid comparison between Unknown and I4
		if (isJammed)
		{
			if (new System.Random().Next(0, 2) == 1)
			{
				isJammed = false;
				MoteMaker.ThrowText(new Vector3((float)((Verb)this).caster.Position.x + 1f, ((Verb)this).caster.Position.y, (float)((Verb)this).caster.Position.z + 1f), ((Verb)this).caster.Map, "Jam Cleared", Color.white, -1f);
				SoundStarter.PlayOneShot(((Thing)((Verb)this).EquipmentSource).def.soundInteract, SoundInfo.op_Implicit(new TargetInfo(((Verb)this).caster.Position, ((Verb)this).caster.Map, false)));
			}
			return;
		}
		QualityUtility.TryGetQuality(((Verb)this).caster, out QualityCategory val);
		if (Rand.Range(1, (int)val switch
		{
			0 => 30,
			2 => 40,
			1 => 50,
			3 => 60,
			4 => 70,
			6 => 80,
			5 => 90,
			7 => 100,
			8 => 10000,
			_ => 60,
		}) == 1)
		{
			MoteMaker.ThrowText(new Vector3((float)((Verb)this).caster.Position.x + 1f, ((Verb)this).caster.Position.y, (float)((Verb)this).caster.Position.z + 1f), ((Verb)this).caster.Map, "Jammed", Color.white, -1f);
			SoundStarter.PlayOneShot(jamSound, SoundInfo.op_Implicit(new TargetInfo(((Verb)this).caster.Position, ((Verb)this).caster.Map, false)));
			isJammed = true;
			return;
		}
		((Verb)this).WarmupComplete();
		if (((Verb)this).CasterIsPawn && ((Verb)this).CasterPawn.skills != null)
		{
			float num2 = 10f;
			if (((LocalTargetInfo)( ((Verb)this).currentTarget)).Thing != null && (int)((LocalTargetInfo)( ((Verb)this).currentTarget)).Thing.def.category == 1)
			{
				num2 = ((!GenHostility.HostileTo(((LocalTargetInfo)( ((Verb)this).currentTarget)).Thing, ((Verb)this).caster)) ? 50f : 240f);
			}
			((Verb)this).CasterPawn.skills.Learn(SkillDefOf.Shooting, num2, false);
		}
	}
}
