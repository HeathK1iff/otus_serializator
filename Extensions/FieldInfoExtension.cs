using System.Globalization;
using System.Reflection;

namespace OtusSerializator.Extensions;
public static class PropertyInfoExtension
{
    public static string GetCSValueFormated(this PropertyInfo propertyInfo, object obj)
    {
        Object objVal = propertyInfo.GetValue(obj);
                
        switch (Type.GetTypeCode(propertyInfo.PropertyType)){
            case TypeCode.DateTime:
                return Convert.ToDateTime(objVal).ToString("ddMMyyyy");
            case TypeCode.Single:
            case TypeCode.Decimal:
            case TypeCode.Double:
                return ToInvariantStringDigit(Convert.ToString(objVal));
            case TypeCode.Empty:
            case TypeCode.Object:
            case TypeCode.DBNull:
                return string.Empty;
            default:
              return Convert.ToString(objVal);
        }
    }

    private static string ToInvariantStringDigit(string value)
    {
        string currentDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        string invariantDecimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator; 
        return value.Replace(currentDecimalSeparator, invariantDecimalSeparator);
    }   

    public static void SetCSFormattedValue(this PropertyInfo propertyInfo, object obj, string value)
    {
        switch (Type.GetTypeCode(propertyInfo.PropertyType))
        {
            case TypeCode.Empty:
            case TypeCode.Object:
            case TypeCode.DBNull:
                propertyInfo.SetValue(obj, null);
                break;
            case TypeCode.Boolean:
                propertyInfo.SetValue(obj, Convert.ToBoolean(value));
                break;
            case TypeCode.Char:
                propertyInfo.SetValue(obj, Convert.ToChar(value));
                break;
            case TypeCode.Byte:
                propertyInfo.SetValue(obj, Convert.ToByte(value));
                break;
            case TypeCode.SByte:
                propertyInfo.SetValue(obj, Convert.ToSByte(value));
                break;
            case TypeCode.UInt16:
                propertyInfo.SetValue(obj, Convert.ToUInt16(value));
                break;
            case TypeCode.UInt32:
                propertyInfo.SetValue(obj, Convert.ToUInt32(value));
                break;
            case TypeCode.UInt64:
                propertyInfo.SetValue(obj, Convert.ToUInt64(value));
                break;
            case TypeCode.Int16:
                propertyInfo.SetValue(obj, Convert.ToInt16(value));
                break;
            case TypeCode.Int32:
                propertyInfo.SetValue(obj, Convert.ToInt32(value));
                break;
            case TypeCode.Int64:
                propertyInfo.SetValue(obj, Convert.ToInt64(value));
                break;
            case TypeCode.Single:
                propertyInfo.SetValue(obj, Convert.ToSingle(FromInvariantStringDigit(value)));
                break;
            case TypeCode.Double:
                propertyInfo.SetValue(obj, Convert.ToDouble(FromInvariantStringDigit(value)));
                break;
            case TypeCode.Decimal:
                propertyInfo.SetValue(obj, Convert.ToDecimal(FromInvariantStringDigit(value)));
                break;
            case TypeCode.DateTime:
                propertyInfo.SetValue(obj,  DateTime.ParseExact(value, "ddMMyyyy", CultureInfo.InvariantCulture));
                break;
            case TypeCode.String:
                propertyInfo.SetValue(obj,  value);
            break;
        }
    }

    private static string FromInvariantStringDigit(string value)
    {
        string currentDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        string invariantDecimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator; 
        return value.Replace(invariantDecimalSeparator, currentDecimalSeparator);
    }
}