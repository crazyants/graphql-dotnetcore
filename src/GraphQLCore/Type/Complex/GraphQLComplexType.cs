﻿namespace GraphQLCore.Type
{
    using Introspection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Translation;
    using Utils;

    public abstract class GraphQLComplexType : GraphQLBaseType
    {
        public GraphQLComplexType(string name, string description) : base(name, description)
        {
            this.Fields = new Dictionary<string, GraphQLObjectTypeFieldInfo>();
        }

        protected Dictionary<string, GraphQLObjectTypeFieldInfo> Fields { get; set; }

        public bool ContainsField(string fieldName)
        {
            return this.Fields.ContainsKey(fieldName);
        }

        public GraphQLObjectTypeFieldInfo GetFieldInfo(string fieldName)
        {
            if (!this.ContainsField(fieldName))
                return null;

            return this.Fields[fieldName];
        }

        public GraphQLObjectTypeFieldInfo[] GetFieldsInfo()
        {
            return this.Fields.Select(e => e.Value)
                .ToArray();
        }

        public override IntrospectedType Introspect(ISchemaObserver schemaObserver)
        {
            var introspectedType = new ComplexIntrospectedType(schemaObserver, this);
            introspectedType.Name = this.Name;
            introspectedType.Description = this.Description;
            introspectedType.Kind = TypeKind.OBJECT;

            return introspectedType;
        }

        protected GraphQLObjectTypeFieldInfo CreateFieldInfo<T, TProperty>(string fieldName, Expression<Func<T, TProperty>> accessor)
        {
            return new GraphQLObjectTypeFieldInfo()
            {
                Name = fieldName,
                IsResolver = false,
                Lambda = accessor,
                Arguments = new Dictionary<string, GraphQLObjectTypeArgumentInfo>(),
                SystemType = ReflectionUtilities.GetReturnValueFromLambdaExpression(accessor)
            };
        }
    }
}