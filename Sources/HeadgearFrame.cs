// Vanya_Headgear.HeadgearFrame
using RimWorld;
using UnityEngine;
using Verse;

public class HeadgearFrame : Apparel
{
	public bool init = false;

	public override void DrawWornExtras()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Invalid comparison between Unknown and I4
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Invalid comparison between Unknown and I4
		if (!base.Wearer.DestroyedOrNull())
		{
			if (!init)
			{
				ApparelGraphicRecord item = default(ApparelGraphicRecord);
				ApparelGraphicRecordGetter.TryGetGraphicApparel((Apparel)this, base.Wearer.story.bodyType, out item);
				base.Wearer.Drawer.renderer.graphics.apparelGraphics.Remove(item);
				PortraitsCache.Clear();
				init = true;
			}
			if (init && base.Wearer == null)
			{
				init = false;
			}
			if (base.Wearer.GetPosture() == PawnPosture.Standing)
			{
				Material material = GraphicDatabase.Get<Graphic_Multi>(def.graphicData.texPath, ShaderDatabase.Cutout, Vector2.one, DrawColor).MatAt(base.Wearer.Rotation);
				Vector3 s = new Vector3(1f, 1f, 1f);
				Matrix4x4 matrix = default(Matrix4x4);
				Vector3 vector = new Vector3(0f, 0f, 0f);
				if ((int)base.Wearer.story.bodyType.index == 1)
				{
					if (base.Wearer.Rotation == Rot4.West)
					{
						vector = new Vector3(-0.05f, 0f, 0f);
					}
					else if (base.Wearer.Rotation == Rot4.East)
					{
						vector = new Vector3(0.05f, 0f, 0f);
					}
				}
				if ((int)base.Wearer.story.bodyType.index != 1)
				{
					if (base.Wearer.Rotation == Rot4.West)
					{
						vector = new Vector3(-0.1f, 0f, 0f);
					}
					else if (base.Wearer.Rotation == Rot4.East)
					{
						vector = new Vector3(0.1f, 0f, 0f);
					}
				}
				if (base.Wearer.Awake() || !base.Wearer.Downed)
				{
					matrix.SetTRS(base.Wearer.DrawPos + new Vector3(0f, 1f, 0.365f) + vector, Quaternion.AngleAxis(0f, Vector3.up), s);
					Graphics.DrawMesh(MeshPool.humanlikeHeadSet.MeshAt(base.Wearer.Rotation), matrix, material, 0);
				}
			}
		}
		if (Find.TickManager.TicksGame % 120 == 0)
		{
			init = false;
		}
		base.DrawWornExtras();
	}

	public override void Tick()
	{
		base.Tick();
		if (base.Wearer.DestroyedOrNull() && init)
		{
			init = false;
		}
	}
}
