using OtusSerializator.Extensions;

namespace OtusSerializator.Utils;

public static class CommaSeparatedValueSerializator
{
    public static string Serialize(object target)
    {
        var values = new List<string>();

        foreach (var property in target.GetType().GetProperties())
        {
            if (IsSkipNestedObject(property.PropertyType))
            {
                continue;
            }

            string val = property.GetCSValueFormated(target);

            values.Add(val);
        }

        return string.Join(',', values);
    }

    public static T Deserialize<T>(string target) where T: class
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException();
        }

        var instance = Activator.CreateInstance<T>();
        var values = target.Split(',');
         
        int nextIndex = 0;
        foreach (var property in instance.GetType().GetProperties())
        {
            if (IsSkipNestedObject(property.PropertyType))
            {
                continue;
            }
            
            property.SetCSFormattedValue(instance, values[nextIndex]);
            nextIndex++;
        }

        return instance;
    }

    private static bool IsSkipNestedObject(Type type)
    {
        return type.IsClass && type != typeof(string);
    }
}
