using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Clinic_Management_Entities;

namespace Clinic_Management_DAL.Infrastractor
{
    public static class DbMapper<T> where T : new()
    {

        private static object? SafeConvert(object value, Type targetType)
        {
            if (value == DBNull.Value)
            {
                // prevent assigning null to non-nullable value types
                if (Nullable.GetUnderlyingType(targetType) == null
                    && targetType.IsValueType)
                    return Activator.CreateInstance(targetType);

                return null;
            }

            var underlying = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlying.IsEnum)
                return Enum.ToObject(underlying, value);

            if (underlying == typeof(Guid))
                return Guid.Parse(value.ToString()!);

            if (underlying == typeof(DateOnly))
                return DateOnly.FromDateTime((DateTime)value);

            if (underlying == typeof(TimeOnly))
                return TimeOnly.FromDateTime((DateTime)value);

            if (underlying.IsAssignableFrom(value.GetType()))
                return value;

            return Convert.ChangeType(value, underlying);
        }



        public static T Map(SqlDataReader reader)
        {
            T obj = new();

            foreach (var prop in typeof(T).GetProperties())
            {
                var attr = prop
                    .GetCustomAttributes(typeof(DbColumnAttribute), false)
                    .FirstOrDefault() as DbColumnAttribute;

                if (attr == null) continue;

                object val = reader[attr.Name];

                var safeValue = SafeConvert(val, prop.PropertyType);

                prop.SetValue(obj, safeValue);

            }

            return obj;
        }
    }



}
