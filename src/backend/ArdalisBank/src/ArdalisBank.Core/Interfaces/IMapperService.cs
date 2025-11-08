namespace ArdalisBank.Core.Interfaces;
public interface IMapperService
{
	public TDestination Map<TDestination>(object source);
	public TDestination Map<TSource, TDestination>(TSource source);
	public TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
	public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);

	public IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(IEnumerable<TSource> source);
	public List<TDestination> MapList<TSource, TDestination>(List<TSource> source);

	public IQueryable<TDestination> MapQuery<TSource, TDestination>(IQueryable<TSource> source);
}