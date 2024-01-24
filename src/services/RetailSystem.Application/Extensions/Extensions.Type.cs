namespace RetailSystem.Application.Extensions;

public static partial class Extensions
{
    public static string ResolveTypeName(this Type type)
    {
        if (!type.IsGenericType) return type.Name;

        var genericTypes = string.Join(",", type.GetGenericArguments().Select(argument => argument.Name));

        var genericTypeName = $"{type.Name.Replace("`", string.Empty)}<{genericTypes}>";

        return genericTypeName;
    }
}
