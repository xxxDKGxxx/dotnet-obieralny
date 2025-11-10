using AutoMapper;

namespace LoanHub.Backend.Core.Interfaces;
public interface IMapFrom<T>
{
	public void Mapping(Profile profile)
	{
		profile.CreateMap(typeof(T), GetType());
	}
}