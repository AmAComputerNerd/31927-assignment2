using HotelSmartManagement.Common.MVVM.Models;

namespace HotelSmartManagement.Common.Helpers
{
    public static class ViewModelHelper
    {
        public static T GetViewModel<T>(this IServiceProvider serviceProvider, object[] constructorParams) where T : class, IViewModel
        {
            return (T)serviceProvider.GetViewModel(typeof(T), constructorParams);
        }

        public static object GetViewModel(this IServiceProvider serviceProvider, Type viewModelType, object[] constructorParams)
        {
            // Get all constructors of type viewModelType.
            var constructors = viewModelType.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"Type {viewModelType} does not have any constructors.");
            }

            // Iterate through all constructors - we are looking for a constructor that has all the required parameters provided.
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length < constructorParams.Length)
                {
                    // This constructor does not have enough parameters to satisfy the provided constructorParams.
                    // We should skip this constructor and try the next one.
                    continue;
                }

                var parameters = constructor.GetParameters();
                var resolvedParameters = new object[parameters.Length];

                var listOfUnresolvedParameters = new List<object>(constructorParams);

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;
                    var paramValue = listOfUnresolvedParameters.FirstOrDefault(p => p.GetType() == paramType);
                    if (paramValue != null)
                    {
                        resolvedParameters[i] = paramValue;
                        listOfUnresolvedParameters.Remove(paramValue);
                    }
                    else
                    {
                        var retrievedService = serviceProvider.GetService(paramType);
                        if (retrievedService == null)
                        {
                            // This constructor has a parameter which doesn't have a matching type in either constructorParams or the service provider.
                            // We should skip this constructor and try the next one.
                            continue;
                        }
                        resolvedParameters[i] = retrievedService;
                    }
                }

                if (listOfUnresolvedParameters.Count > 0)
                {
                    // This constructor doesn't use up all the parameters provided in constructorParams.
                    // We should skip this constructor and try the next one.
                    continue;
                }

#nullable disable // Reason: We have already checked if the constructor has enough parameters to satisfy the provided constructorParams.
                return Activator.CreateInstance(viewModelType, resolvedParameters);
#nullable enable // Reason: We have already checked if the constructor has enough parameters to satisfy the provided constructorParams.
            }

            // There are no constructors that can be satisfied with the provided constructorParams.
            throw new InvalidOperationException($"Type {viewModelType} does not have any constructors that would satisfy the parameters provided.");
        }
    }
}
