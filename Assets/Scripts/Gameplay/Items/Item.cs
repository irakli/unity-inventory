﻿/*
 *	Created on 8/10/2017 5:31:46 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using UI;
using UnityEngine;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Item : ITooltipSerializable
	{
		public int ID { get; set; }
		public int Value { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		public bool Stackable { get; set; }
		public Sprite Sprite { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Slug)}: {Slug}, {nameof(Value)}: {Value}";
		}

		/// <inheritdoc />
		public abstract string ToTooltip();
	}
}