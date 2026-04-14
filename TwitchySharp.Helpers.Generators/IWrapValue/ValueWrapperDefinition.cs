using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading;

namespace TwitchySharp.Helpers.Generators;
internal record ValueWrapperDefinition
{
    public static ValueWrapperDefinition? FromAttributeContext(
        GeneratorAttributeSyntaxContext context,
        CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (context.TargetSymbol is not INamedTypeSymbol wrapperTypeSymbol)
            return null;
        ct.ThrowIfCancellationRequested();
        if (context.Attributes.First().AttributeClass?.TypeArguments.FirstOrDefault() is not INamedTypeSymbol wrappedTypeSymbol)
            return null;
        ct.ThrowIfCancellationRequested();
        return FromTypeSymbol(
            wrapperTypeSymbol,
            wrappedTypeSymbol,
            context.SemanticModel.Compilation.GetTypeByMetadataName(ValueWrapperConstants.JSON_CONVERTER_ATTRIBUTE_TYPE_NAME),
            ct
            );
    }
    public static ValueWrapperDefinition FromTypeSymbol(
        INamedTypeSymbol typeSymbol,
        INamedTypeSymbol wrappedTypeSymbol,
        INamedTypeSymbol? jsonConverterAttributeSymbol,
        CancellationToken ct)
        => new()
        {
            Namespace = typeSymbol.ContainingNamespace.ToDisplayString(),
            Name = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            IsPartial = typeSymbol.IsPartial(),
            IsRecord = typeSymbol.IsRecord,
            PartialDeclaration = typeSymbol.ToPartialTypeDeclarationString(),
            WrappedType = WrappedTypeInfo.FromTypeSymbol(wrappedTypeSymbol, ct),
            ParentTypes = typeSymbol.GetContainingTypes().Select(t => ParentTypeDefinition.FromTypeSymbol(t, ct)).ToRecordImmutableArray(),
            WrapperConstructor = ValueWrapperConstructorInfo.FromWrapperType(typeSymbol, wrappedTypeSymbol, ct),
            WrapperValueProperty = WrappedTypeValueMemberInfo.FromTypeSymbol(typeSymbol, wrappedTypeSymbol, ct),
            ImplicitOperator = ImplicitOperatorInfo.FromTypeSymbol(typeSymbol, wrappedTypeSymbol, ct),
            ToStringOverride = ToStringOverrideInfo.FromTypeSymbol(typeSymbol, ct),
            HasJsonConverterAttribute = typeSymbol.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, jsonConverterAttributeSymbol)),
            StaticCreateMethod = StaticCreateMethodInfo.FromTypeSymbol(typeSymbol, wrappedTypeSymbol, ct)
        };

    public readonly record struct WrappedTypeInfo
    {
        private static readonly SymbolDisplayFormat RealFullyQualifiedFormat = // string => global::System.String
            SymbolDisplayFormat.FullyQualifiedFormat.WithMiscellaneousOptions(
            SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions &
            ~SymbolDisplayMiscellaneousOptions.UseSpecialTypes
            );
        public static WrappedTypeInfo FromTypeSymbol(INamedTypeSymbol symbol, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return new()
            {
                FullyQualifiedName = symbol.ToDisplayString(RealFullyQualifiedFormat)
            };
        }
        public required string FullyQualifiedName { get; init; }
    }
    public readonly record struct ValueWrapperConstructorInfo
    {
        public static ValueWrapperConstructorInfo? FromWrapperType(
            INamedTypeSymbol symbol,
            INamedTypeSymbol wrappedTypeSymbol, 
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            if (symbol.InstanceConstructors
                .Where(c => c.Parameters.Length == 1)
                .Where(c => SymbolEqualityComparer.Default.Equals(c.Parameters.Single().Type, wrappedTypeSymbol))
                .Select(c => (IMethodSymbol?)c)
                .FirstOrDefault() is not IMethodSymbol constructor)
                return null;
            return new()
            {
                WrappedValueParameterName = constructor.Parameters.Single().Name,
                IsPrimaryConstructor = constructor.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax(ct) is TypeDeclarationSyntax
            };
        }
        public required string WrappedValueParameterName { get; init; }
        public required bool IsPrimaryConstructor { get; init; }
    }
    public readonly record struct WrappedTypeValueMemberInfo
    {
        public static WrappedTypeValueMemberInfo? FromTypeSymbol(
            INamedTypeSymbol symbol,
            INamedTypeSymbol wrappedTypeSymbol,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            if (symbol.GetMembers("Value")
                .OfType<IPropertySymbol>()
                .FirstOrDefault(p => SymbolEqualityComparer.Default.Equals(p.Type, wrappedTypeSymbol)) is not IPropertySymbol valueProperty)
                return null;
            ct.ThrowIfCancellationRequested();
            return new()
            {
                Name = valueProperty.Name,
                Initializable = valueProperty.SetMethod switch
                {
                    { DeclaredAccessibility: Accessibility.Public or Accessibility.Internal } => true,
                    _ => false
                }
            };
        }
        public required string Name { get; init; }
        public required bool Initializable { get; init; }
    }
    public readonly record struct ToStringOverrideInfo
    {
        public static ToStringOverrideInfo? FromTypeSymbol(INamedTypeSymbol symbol, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            if (symbol.GetMembers("ToString")
                .OfType<IMethodSymbol>()
                .Where(m => !m.IsImplicitlyDeclared)
                .FirstOrDefault(m => m.Parameters.Length == 0 && m.IsOverride) is null)
                return null;
            return new();
        }
    }
    public readonly record struct ImplicitOperatorInfo
    {
        public static ImplicitOperatorInfo? FromTypeSymbol(
            INamedTypeSymbol symbol,
            INamedTypeSymbol wrappedTypeSymbol,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            if (symbol.GetMembers()
                .OfType<IMethodSymbol>()
                .FirstOrDefault(m => m.MethodKind == MethodKind.Conversion
                    && m.Name == "op_Implicit"
                    && SymbolEqualityComparer.Default.Equals(m.ReturnType, wrappedTypeSymbol)) is null)
                return null;
            return new();
        }
    }
    public readonly record struct StaticCreateMethodInfo
    {
        public static StaticCreateMethodInfo? FromTypeSymbol(
            INamedTypeSymbol symbol,
            INamedTypeSymbol wrappedTypeSymbol,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            if (symbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .FirstOrDefault(m => m.IsStatic
                    && m.Name == "Create"
                    && m.Parameters.Length == 1
                    && SymbolEqualityComparer.Default.Equals(m.ReturnType, symbol)
                    && SymbolEqualityComparer.Default.Equals(m.Parameters.Single(), wrappedTypeSymbol)) is null)
                return null;
            return new();
        }
    }
    public record ParentTypeDefinition
    {
        public static ParentTypeDefinition FromTypeSymbol(INamedTypeSymbol parentTypeSymbol, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return new()
            {
                PartialDeclaration = parentTypeSymbol.ToPartialTypeDeclarationString(),
                IsPartial = parentTypeSymbol.IsPartial()
            };
        }
        public required string PartialDeclaration { get; init; }
        public required bool IsPartial { get; init; }
    }

    public required string Namespace { get; init; }
    public required string Name { get; init; }
    public required string PartialDeclaration { get; init; }
    public required bool IsPartial { get; init; }
    public required bool IsRecord { get; init; }
    public required bool HasJsonConverterAttribute { get; init; }
    public RecordImmutableArray<ParentTypeDefinition> ParentTypes { get; init; } = new();
    public required WrappedTypeInfo WrappedType { get; init; }
    public ValueWrapperConstructorInfo? WrapperConstructor { get; init; }
    public WrappedTypeValueMemberInfo? WrapperValueProperty { get; init; }
    public ToStringOverrideInfo? ToStringOverride { get; init; }
    public ImplicitOperatorInfo? ImplicitOperator { get; init; }
    public StaticCreateMethodInfo? StaticCreateMethod { get; init; }
    public bool ShouldAddValueProperty => this switch
    {
        { IsRecord: true, WrapperConstructor: not null } or { WrapperValueProperty: not null } => false,
        _ => true
    };
}