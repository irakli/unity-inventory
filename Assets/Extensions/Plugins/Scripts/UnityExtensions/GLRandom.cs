﻿// Copyright Gamelogic (c) http://www.gamelogic.co.za

using Gamelogic.Extensions.Internal;
using UnityEngine;
using Random = System.Random;

namespace Gamelogic.Extensions
{
	/// <summary>
	///     Some convenience functions for random bools and integers.
	/// </summary>
	[Version(1, 2)]
	public static class GLRandom
	{
		#region Constants

		/// <summary>
		///     Globally accessible <see cref="System.Random" /> object for random calls
		/// </summary>
		public static readonly IRandom GlobalRandom = new RandomImpl();

		#endregion

		#region  Types

		private class RandomImpl : IRandom
		{
			#region Private Fields

			private readonly Random random;

			#endregion

			#region Constructors

			public RandomImpl()
			{
				random = new Random();
			}

			public RandomImpl(int seed)
			{
				random = new Random(seed);
			}

			#endregion

			#region Public Methods

			public double NextDouble()
			{
				return random.NextDouble();
			}

			public int Next()
			{
				return random.Next();
			}

			public int Next(int maxValue)
			{
				return random.Next(maxValue);
			}

			public int Next(int minValue, int maxValue)
			{
				return random.Next(minValue, maxValue);
			}

			public override string ToString()
			{
				return random.ToString();
			}

			public void NextBytes(byte[] bytes)
			{
				random.NextBytes(bytes);
			}

			/// <summary>
			///     Returns a point randomly selected the on a sphere.
			/// </summary>
			/// <param name="radius">The radius of the sphere.</param>
			/// <returns></returns>
			// http://mathworld.wolfram.com/SpherePointPicking.html
			public Vector3 RandomOnSphere(float radius)
			{
				var u = (float) random.NextDouble();
				var v = (float) random.NextDouble();

				var theta = 2 * Mathf.PI * u;
				var phi = Mathf.Acos(2 * v - 1);

				var x = radius * Mathf.Cos(theta) * Mathf.Sin(phi);
				var y = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
				var z = radius * Mathf.Cos(phi);

				return new Vector3(x, y, z);
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		///     Generates either -1.0f or 1.0f randomly.
		/// </summary>
		/// <returns></returns>
		public static float Sign()
		{
			return Bool(0.5f) ? -1.0f : 1.0f;
		}

		/// <summary>
		///     Generates a random bool, true with the given probability.
		/// </summary>
		/// <param name="probability"></param>
		/// <returns></returns>
		public static bool Bool(float probability)
		{
			return GlobalRandom.NextDouble() < probability;
		}

		/// <summary>
		///     Generates a Random integer between 0 inclusive and the given max, exclusive.
		/// </summary>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Range(int max)
		{
			return GlobalRandom.Next(max);
		}

		/// <summary>
		///     Generates a Random integer between the given min inclusive and the given max, exclusive.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Range(int min, int max)
		{
			return GlobalRandom.Next(min, max);
		}

		/// <summary>
		///     Generates a Random float between 0.0f inclusive and the given max
		/// </summary>
		/// <param name="max"></param>
		/// <returns></returns>
		public static float Range(float max)
		{
			return (float) GlobalRandom.NextDouble() * max;
		}

		/// <summary>
		///     Generates a Random float between the given min inclusive and the given max, exclusive.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static float Range(float min, float max)
		{
			return Range(max - min) + min;
		}

		/// <summary>
		///     Gives a random value within a given range centered around a given value.
		/// </summary>
		/// <param name="value">The value around which the random values will be centered.</param>
		/// <param name="range">The range of the returned value.</param>
		/// <returns>A random value between value - range/2 and value + range/2.</returns>
		public static float RandomOffset(float value, float range)
		{
			var offset = GlobalRandom.NextDouble() * range - range / 2;
			return (float) (value + offset);
		}

		/// <summary>
		///     Gets a new random generator.
		/// </summary>
		/// <returns>A new random generator</returns>
		public static IRandom GetRandom()
		{
			return new RandomImpl();
		}

		/// <summary>
		///     Gets a new random generator.
		/// </summary>
		/// <param name="seed">The seed to instantiate the generator with.</param>
		/// <returns>A seeded instance of a random generator.</returns>
		public static IRandom GetRandom(int seed)
		{
			return new RandomImpl(seed);
		}

		#endregion
	}

	/// <summary>
	///     Represents a random generator.
	/// </summary>
	[Version(1, 5)]
	public interface IRandom
	{
		#region Public Methods		

		/// <summary>
		///     Gets the next the random double value.
		/// </summary>
		double NextDouble();

		/// <summary>
		///     Gets the next the random integer value.
		/// </summary>
		int Next();

		/// <summary>
		///     Gets the next the random integer value below the given maximum.
		/// </summary>
		int Next(int maxValue);

		/// <summary>
		///     Gets the next the random integer value greater than or equal to the minimum
		///     and below the given maximum.
		/// </summary>
		int Next(int minValue, int maxValue);

		/// <summary>
		///     Fills the given array with random bytes.
		/// </summary>
		void NextBytes(byte[] bytes);


		/// <summary>
		///     Returns a random value on the surface of a sphere with a given radius.
		/// </summary>
		/// <param name="radius">The radius.</param>
		/// <returns>Vector3.</returns>
		Vector3 RandomOnSphere(float radius);

		#endregion
	}
}