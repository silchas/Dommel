﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Dommel
{
    public static partial class DommelMapper
    {
        /// <summary>
        /// Defines methods for resolving the key property of entities.
        /// Custom implementations can be registerd with <see cref="SetKeyPropertyResolver(IKeyPropertyResolver)"/>.
        /// </summary>
        public interface IKeyPropertyResolver
        {
            /// <summary>
            /// Resolves the key properties for the specified type.
            /// </summary>
            /// <param name="type">The type to resolve the key properties for.</param>
            /// <returns>A collection of <see cref="PropertyInfo"/> instances of the key properties of <paramref name="type"/>.</returns>
            KeyPropertyInfo[] ResolveKeyProperties(Type type);
        }
    }

    /// <summary>
    /// Represents the key property of an entity type.
    /// </summary>
    public class KeyPropertyInfo
    {
        /// <summary>
        /// Initializes a new <see cref="KeyPropertyInfo"/> instance from the 
        /// specified <see cref="PropertyInfo"/> instance.
        /// </summary>
        /// <param name="property">
        /// The property which represents the key property. The <see cref="DatabaseGeneratedOption"/> is 
        /// determined from the value of the <see cref="DatabaseGeneratedAttribute"/> specified on
        /// the property.
        /// </param>
        public KeyPropertyInfo(PropertyInfo property)
        {
            Property = property;
            GeneratedOption = property.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption
                ?? DatabaseGeneratedOption.Identity;
        }

        /// <summary>
        /// Initializes a new <see cref="KeyPropertyInfo"/> instance from the 
        /// specified <see cref="PropertyInfo"/> instance using the specified
        /// <see cref="DatabaseGeneratedOption"/>.
        /// </summary>
        /// <param name="property">The property which represents the key property.</param>
        /// <param name="generatedOption">
        /// The <see cref="DatabaseGeneratedOption"/> which specifies whether the value of
        /// the column this property represents is generated by the database or not.
        /// </param>
        public KeyPropertyInfo(PropertyInfo property, DatabaseGeneratedOption generatedOption)
        {
            Property = property;
            GeneratedOption = generatedOption;
        }

        /// <summary>
        /// Gets a reference to the property of this key property.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Gets the <see cref="DatabaseGeneratedOption"/> which specifies whether the value of
        /// the column this property represents is generated by the database or not.
        /// </summary>
        public DatabaseGeneratedOption GeneratedOption { get; }

        /// <summary>
        /// Gets a value indicating whether this key property's value is generated by the database.
        /// </summary>
        public bool IsGenerated => GeneratedOption != DatabaseGeneratedOption.None;
    }
}
