namespace Abilities
{
	public interface IAbility
	{
		int ID { get; }
		string Name { get; }

		void Use();
	}
}