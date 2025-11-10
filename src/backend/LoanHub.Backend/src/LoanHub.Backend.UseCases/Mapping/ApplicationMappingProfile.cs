using System.Reflection;
using LoanHub.Backend.Core.Interfaces;
using AutoMapper;

namespace LoanHub.Backend.UseCases.Mapping;
public class ApplicationMappingProfile : Profile
{
	public ApplicationMappingProfile()
	{
		ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
	}

	private void ApplyMappingsFromAssembly(Assembly assembly)
	{
		var types = assembly.GetExportedTypes();

		foreach (var type in types)
		{
			var mapFromInterface = type.GetInterfaces()
				.FirstOrDefault(i =>
				{
					return i.IsGenericType &&
														i.GetGenericTypeDefinition() == typeof(IMapFrom<>);
				});
			if (mapFromInterface != null)
			{
				var method = mapFromInterface.GetMethod("Mapping")!;
				var instance = Activator.CreateInstance(type);
				method.Invoke(instance, [this]);
				continue;
			}

			var mapToInterface = type.GetInterfaces()
				.FirstOrDefault(i =>
				{
					return i.IsGenericType &&
														i.GetGenericTypeDefinition() == typeof(IMapTo<>);
				});
			if (mapToInterface != null)
			{
				var method = mapToInterface.GetMethod("Mapping")!;
				var instance = Activator.CreateInstance(type);
				method.Invoke(instance, [this]);
			}
		}
	}
}