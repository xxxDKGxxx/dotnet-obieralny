using AutoMapper;

namespace ArdalisBank.Core.Interfaces;
public interface IMapTo<T>
{
	public void Mapping(Profile profile)
	{
		profile.CreateMap(GetType(), typeof(T));
	}
}