﻿using System.ComponentModel;
using System.Globalization;
using SharedLibrary.Models;

namespace SharedLibrary.Convertors;

public class FieldTypeTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(int) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return value switch
        {
            string stringValue => FieldType.TryGet(stringValue),
            int intValue => FieldType.TryGet(intValue),
            _ => base.ConvertFrom(context, culture, value)
        };
    }
}