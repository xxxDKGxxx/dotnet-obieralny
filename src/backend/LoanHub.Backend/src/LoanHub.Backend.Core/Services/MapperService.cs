using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Core.Services;
public class MapperService : IMapperService
{
	private readonly IMapper _mapper;
	private readonly IConfigurationProvider _configurationProvider;

	public MapperService(IMapper mapper)
	{
		_mapper = mapper;
		_configurationProvider = mapper.ConfigurationProvider;
	}

	public TDestination Map<TDestination>(object source)
	{
		return _mapper.Map<TDestination>(source);
	}

	public TDestination Map<TSource, TDestination>(TSource source)
	{
		return _mapper.Map<TSource, TDestination>(source);
	}

	public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
	{
		return _mapper.Map(source, destination);
	}

	public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
	{
		return source.ProjectTo<TDestination>(_configurationProvider);
	}

	public IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(IEnumerable<TSource> source)
	{
		return _mapper.Map<IEnumerable<TDestination>>(source);
	}

	public List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
	{
		return _mapper.Map<List<TDestination>>(source);
	}

	public IQueryable<TDestination> MapQuery<TSource, TDestination>(IQueryable<TSource> source)
	{
		return source.ProjectTo<TDestination>(_configurationProvider);
	}
}