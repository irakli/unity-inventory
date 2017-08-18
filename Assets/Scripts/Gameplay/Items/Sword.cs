/*
 *	Created on 8/10/2017 6:12:34 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using UnityEngine;
using Utilities;

namespace Gameplay.Items
{
	[Serializable]
	public class Sword : Weapon
	{
		public Sword(int id, string name, string description, string slug, int value, bool stackable, int damage, int range)
		{
			ID = id;
			Value = value;
			Name = name;
			Description = description;
			Slug = slug;
			Damage = damage;
			Range = range;
			Stackable = stackable;

			Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
		}

		public Sword()
		{
		}

		public int Range { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $", {nameof(Range)}: {Range}";
		}

		/// <inheritdoc />
		public override void Drop()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public override void PickUp()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public override string ToHTML(Color nameColor, Color descriptionColor, Color valueColor)
		{
			return $"<color=#{nameColor.ToHex()}><b>" + Name + "</b></color>\n\n" + $"<color=#{descriptionColor.ToHex()}>" +
			       Description + "</color>\n" + $"<color=#{valueColor.ToHex()}>" + "Value: " +
			       Value + "</color>";
		}
	}
}