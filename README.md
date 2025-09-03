# 🏠 MillionLuxury Real Estate API

Un sistema API RESTful moderno para la gestión de propiedades inmobiliarias de lujo, desarrollado con .NET 9 y las mejores prácticas de la industria.

**Aclaracion 🗣️**:
Se que este proyecto pudo ser ejecutado de manera muy simple, incluso sin ningun tipo de capa, acoplando la logica a los controllers o endpoints; **sin embargo con este proyecto pretendo dar una muestra de mis capacidades tecnicas para desarrollo de alta calidad en entornos productivos, profesionales y vanguardistas.**

## 🌟 Vista General

Este proyecto representa una implementación completa de un sistema de gestión inmobiliaria que demuestra arquitectura limpia, patrones de diseño modernos y las prácticas de desarrollo empresarial profesional que he aprendido a lo largo de mi experiencia. El sistema permite la gestión de propiedades, incluyendo creación, actualización, consulta avanzada y procesamiento de imágenes en segundo plano (valor añadido).

## 🏗️ Arquitectura del Proyecto

El proyecto sigue una **arquitectura limpia (Clean Architecture)** organizada en capas bien definidas:

```
src/
├── MillionLuxury.RealEstate.API/          # Capa de Presentación
├── MillionLuxury.RealEstate.Application/  # Capa de Aplicación  
├── MillionLuxury.RealEstate.Domain/       # Capa de Dominio
└── MillionLuxury.RealEstate.Infrastructure/ # Capa de Infraestructura
```

### 📋 Estructura Detallada

- **`tests/`** - Pruebas unitarias (al principio queria hacer de integracion pero por tiempo no lo hice)
- **`tools/`** - Herramientas de desarrollo y despliegue
  - **`Docker/`** - Configuración de contenedores (Keycloak, bases de datos)
  - **`Scripts/`** - Scripts para migraciones y tareas comunes

## 🚀 Cómo Ejecutar el Proyecto

## 🚀 Inicio Rápido

### Usando Docker Compose directamente
```bash
# Desde la raíz del proyecto
docker-compose -f tools/Docker/docker-file.yml up -d
```

_Nota: Se paciente, tomate un cafe ☕, en mi local duro unos 15 minutos la primera vez, cuando descarga los services._

**‼️IMPORTANTE‼️**: Si estas usando las collections de postman que incluí en el repositorio, recuerda cambiar le puerto a localhost:8081 que es en el puerto en el que por default correra el servicio backend en docker compose.

### Desarrollo Local (Infraestructura en Docker + API local)

### Prerrequisitos

- .NET 9 SDK
- Docker y Docker Compose
- SQL Server (Local o contenedor)
- Visual Studio 2022 o VS Code

### 1. Configurar Servicios de Infraestructura

```powershell
# Navegar al directorio de Docker
cd tools/Docker

# Levantar los servicios (Keycloak + PostgreSQL)
docker-compose -f docker-file.yml up -d
```

#### Configurar Keycloak: audience en el token

Por experiencia, el mapeo declarativo de claims a "audience" puede no funcionar siempre, si ese es tu caso y empiezas a recibir errores de audience con el AuthZ realiza alguno de los siguientes pasos:

1) Opción rápida (desarrollo): deshabilitar la validación de audience en `appsettings.json`.

```json
{
    "KeycloakConfiguration": {
        "Authority": "http://localhost:8080/realms/realestate",
        "Audience": "realestate-api",
        "RequireHttpsMetadata": false,
        "ValidateIssuer": true,
        "ValidateAudience": false, // deshabilitado en local
        "ValidateLifetime": true,
        "ValidateIssuerSigningKey": true,
        "ClockSkew": "00:05:00"
    }
}
```

2) Opción recomendada (producción): agregar un mapper de tipo Audience en Keycloak.

Pasos en Keycloak (instancia local):

1. Abre: http://localhost:8080/admin
2. Inicia sesión: usuario `admin`, contraseña `admin`.
3. Ingresa al realm: `realestate`.
4. Ve a: Clients → selecciona `realestate-api`.
5. Pestaña: Client scopes → selecciona `realestate-api-dedicated`.
6. Click en: Add Mapper → By configuration.
7. Selecciona: "Audience" y configura:
     - included.custom.audience: `realestate-api`
     - access.token.claim: `true`
     - id.token.claim: `false` (opcional)
8. Guarda los cambios.

En mi flujo, para avanzar rápido en desarrollo uso la opción 1 y dejo la 2 para ambientes formales.

### 2. Configurar Base de Datos

```powershell
# Desde la raíz del proyecto
# Ejecutar migraciones (usar el script incluido)
.\tools\Scripts\db-update.sh

# O manualmente:
dotnet ef database update --project src/MillionLuxury.RealEstate.Infrastructure --startup-project src/MillionLuxury.RealEstate.API
```

### 3. Ejecutar la Aplicación

Usa tu IDE preferido para correrla, yo use Visual Studio, para lo cual deberias abrir la solution MillionLuxury.RealEstate.sln
Configurar el project MillionLuxury.RealEstate.API como startup project y ejecutar.

### 4. Acceder a los Servicios

- **API**: `https://localhost:7043` o `http://localhost:5043`
- **Swagger UI**: `https://localhost:7043/swagger`
- **Keycloak Admin**: `http://localhost:8080` 
  - Usuario: `admin` 
  - Contraseña: `admin`
- **Hangfire Dashboard**: `https://localhost:7043/hangfire`

### 5. Obtener Token de Autenticación

Recomiendo usar la colección de postman adjunta en el repositorio.

La colección no es nada sofisticado y tiene las URLs quemadas, pero funciona perfecto para testear rapido.

## 🎯 Características Principales

- ✅ **CRUD Completo** de propiedades inmobiliarias
- ✅ **Filtrado y Paginación** avanzada con ordenamiento dinámico
- ✅ **Gestión de Imágenes** con procesamiento en segundo plano
- ✅ **Autenticación JWT** con Keycloak
- ✅ **Health Checks** completos con métricas detalladas
- ✅ **Documentación OpenAPI** con soporte de autenticación
- ✅ **Compresión de Imágenes** optimizada

## 📚 Endpoints Principales

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/properties` | Listar propiedades con filtros |
| POST | `/api/properties` | Crear nueva propiedad |
| POST | `/api/properties/{id}/images` | Subir imagen individual |
| POST | `/api/properties/{id}/images/batch` | Subir múltiples imágenes (async) |
| PUT | `/api/properties/{id}/price` | Actualizar precio |
| PATCH | `/api/properties/{id}` | Actualización parcial |
| GET | `/api/job-status/{jobId}` | Consultar estado de trabajos |
| GET | `/api/health` | Health check básico |
| GET | `/api/health/detailed` | Health check detallado |

---

# 🌟 ¿Por Qué Este Proyecto Es Excepcional?

*Permíteme contarte por qué este proyecto es una excelente representacion del desarrollo profesional...*

## 💎 Un Ejemplo de Arquitectura Limpia Real

Este no es solo otro CRUD básico. Es una **demostración completa de cómo se construye software empresarial de calidad**. Cada línea de código está pensada para ser mantenible, escalable y robusta, para darles a ustedes una excelente muestra de mis habilidades.

### 🔧 Minimal APIs: La Modernidad en Acción

Adopté las **Minimal APIs de .NET**, pero no de forma ingenua. Cada endpoint lo implementé como métodos estáticos con **"Skinny Endpoints"**: endpoints delgados que delegan inmediatamente la lógica a los casos de uso apropiados:

```csharp
public static async Task<IResult> CreatePropertyAsync(
    CreatePropertyBuildingRequest request,
    ICreatePropertyBuildingUseCase useCase,
    CancellationToken cancellationToken)
{
    var response = await useCase.ExecuteAsync(request, cancellationToken);
    return Results.Created($"/{ApiRoutes.PropertyResource}{response.Id}", response);
}
```

**¿Porque es util este enfoque?** Los endpoints son **puramente de transporte**, sin lógica de negocio. Esto mantiene la separación de responsabilidades y facilita las pruebas unitarias.

## 🏛️ Inyección de Dependencias: Organización Maestra

No vas a ver un `Program.cs` sobrecargado aquí. Implementé **DI padre por capa** con extensiones organizadas:

```csharp
// Program.cs - Limpio y expresivo
builder.Services.AddApplication();
builder.Services.AddInfrastructure(config);
builder.Services.AddApi(builder);
```

Diseñé cada capa con su propio `DependencyInjection.cs` y **extensiones especializadas** de **responsabilidad única**. Repliqué este patrón en todas las capas para mantener el código fácil de extender y mantener.

## 🛡️ Seguridad Empresarial con Keycloak

No me conformé con autenticación básica. Integré **Keycloak** como servidor de identidad profesional para dar un toque profesional y ampliamente tecnico al producto final:

- **JWT Bearer Authentication** completamente configurado
- **Realm personalizado** para el dominio inmobiliario
- **Swagger UI** con soporte nativo para tokens Bearer
- **Configuración centralizada** y reutilizable

```csharp
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter the JWT token in the format: Bearer {token}"
});
```

## 🎯 Domain-Driven Design: Implementacion parcial

### Value Objects: Modelado Inteligente

Implementé **Value Objects** como `Address` para encapsular conceptos del dominio:

```csharp
public record Address(string Country, string City, string Street, int ZipCode);
```

### Anemic vs Rich Models: Decisión Arquitectónica Consciente

**¿Por qué elegí anemic models?** En este proyecto, las entidades son relativamente simples y no requieren reglas complejas. Elegí **anemic models** con la lógica en **casos de uso** por coherencia y simplicidad. Para sistemas con reglas ricas, mi preferencia es evolucionar hacia **rich domain models**.

## 🗄️ Repositorios: Genéricos + Especializados

Busqué la combinación perfecta de **DRY** y **funcionalidad específica**:

```csharp
// Repositorio base genérico - Evita repetición
public class BaseGenericRepository<TEntity> : IBaseGenericRepository<TEntity>
    where TEntity : BaseEntity

// Repositorios específicos para consultas complejas
public async Task<(IEnumerable<Property>, int)> GetFilteredAsync(ListPropertiesRequest request)
```

**El resultado:** Cero duplicación de código para operaciones básicas, pero funcionalidad completa para consultas especializadas.

## 🔄 Casos de Uso: Segregación Perfecta de Responsabilidades

Encapsulé cada operación de negocio en su propio caso de uso:

```csharp
public class CreatePropertyBuildingUseCase : ICreatePropertyBuildingUseCase
{
    // Lógica pura de negocio, sin preocupaciones de infraestructura
}
```

**Beneficios:**
- **Testing** fácil y aislado
- **Reutilización** de lógica de negocio
- **Mantenimiento** simplificado
- **Single Responsibility Principle** aplicado religiosamente

## ⚡ Procesamiento Asíncrono Profesional

### Hangfire: Trabajos en Segundo Plano Elegantes

Aparte del endpoint que se pidio para subir imagenes, decidí enriquecer mi entrega con un endpoints que permita subir varias, mostrando mis habilidades para lidiar con diferentes situaciones y caracteristicas.

La subida de múltiples imágenes no bloquea la API. Uso **Hangfire** para el procesamiento asíncrono:

```csharp
public static async Task<IResult> AddMultiplePropertyImagesAsync(...)
{
    var response = await useCase.ExecuteAsync(request, cancellationToken);
    return Results.Accepted($"{ApiRoutes.Base}{ApiRoutes.JobStatusResource}{response.JobId}", response);
}
```

**¿Por qué elegí Hangfire?** Por simplicidad y elegancia, puede no ser el enfoque de diseño ideal para un producto productivo, pero para un proyecto simple, funciona perfecto!

**¿Por qué es una buena implementacion?** Respuesta inmediata `202 Accepted` con endpoint para consultar el progreso. **Exactamente** como funcionan tipicamente las APIs profesionales.

## 🗜️ Compresión de Imágenes: Equilibrio Técnico Perfecto

Implementé **Brotli compression** para imágenes:

```csharp
using (var brotliStream = new BrotliStream(compressedStream, CompressionLevel.Optimal))
{
    await brotliStream.WriteAsync(imageData);
}
```

**El equilibrio perfecto:** Ahorro significativo de espacio en disco sin sobrecargar el servidor.

## 🔍 Consultas Dinámicas:

Sistema de filtrado y ordenamiento completamente dinámico:

```csharp
private static IQueryable<Property> ApplySorting(IQueryable<Property> query, string? sortBy, bool descending)
{
    return sortBy?.ToLowerInvariant() switch {
        "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
        "year" => descending ? query.OrderByDescending(p => p.Year) : query.OrderBy(p => p.Year),
        // ...más opciones
        _ => query.OrderByDescending(p => p.CreatedAt)
    };
}
```

## 🛡️ Validación con FluentValidation: Validaciones elegantes

Promoví las validaciones a la **capa de aplicación** para mantener alta cohesión:

```csharp
RuleFor(x => x.Name)
    .NotEmpty()
    .WithMessage("Property name is required.")
    .MinimumLength(MinNameLength)
    .MaximumLength(MaxNameLength);
```

**Resultado:** Validaciones expresivas, reutilizables y fácilmente testeable.

## 🗃️ Entity Framework: Interceptors y Filtros Globales

### Auditoría Automática Elegante

```csharp
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    // Auditoría automática en cada operación
    // Soft delete implementado transparentemente
}
```

### Filtros Globales para Soft Delete

```csharp
builder.Entity(softDeleteEntity).HasQueryFilter(GenerateQueryFilterLambda(softDeleteEntity));
```

**¿El resultado?** Las entidades eliminadas **nunca** aparecen en consultas sin configuración manual. Transparencia total.

## 🏥 Health Checks: Monitoreo Profesional

En diseño he implementacion de sistemas no solo nos interesa saber si "está funcionando", sino **cómo está funcionando**:

```csharp
group.MapHealthChecks("/detailed", new HealthCheckOptions {
    ResponseWriter = WriteDetailedResponseAsync
});
```

## 💽 Data Seeding: Moderno y Confiable

Diseñé un sistema de seed data **determinista**, **elegante** y **sencillo**:

```csharp
public static class DataSeedManager
{
    public static DateTime SeedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static string SeedUser = "DataSeed";
}
```

## 📝 Records para DTOs: C# Moderno

Usé `records` de forma extensiva para DTOs **inmutables**:

```csharp
public record CreatePropertyBuildingRequest(
    string Name,
    string Country,
    // ... más propiedades
);
```

**Beneficio:** Inmutabilidad por defecto, comparación estructural automática, menos boilerplate.

## ⚰️ Responsabilidad de Persistencia: Delegando responsabilidades correctamente

Decidí que los repositorios **NO** guarden automáticamente en cada metodo. Para ello delego la persistencia en **componentes de alto nivel**:

```csharp
// El repositorio prepara, el caso de uso decide cuándo persistir
var property = await _repository.AddAsync(newProperty);
await _repository.SaveChangesAsync(); // Control explícito del scope transaccional
```

Esto permite que la decision de persistencia sea dominio propio del componente de alto nivel que hace uso de los repositorios, usualmente el caso de uso.

## 🎨 .editorconfig: Consistencia de Código

Mantengo reglas de formato **modernas** y **establecidas**:

```editorconfig
csharp_style_namespace_declarations = file_scoped:error
csharp_style_var_elsewhere = true:suggestion
dotnet_style_qualification_for_field = true:suggestion
```

## 🐳 Herramientas de Desarrollo

### Docker Compose Completo
- **Keycloak** con PostgreSQL
- **Configuración de realm** automática
- **Healthchecks** para todos los servicios

### Scripts de Automatización
```bash
# Crear migración
.\tools\Scripts\db-create-migration.sh "NombreMigracion"

# Actualizar base de datos  
.\tools\Scripts\db-update.sh
```

---

## 🎉 El Resultado Final

Para mí, este proyecto no es solo código que funciona. Es una **demostración de maestría todas mis habilidades** que combina:

- ✅ **Arquitectura limpia** aplicada correctamente
- ✅ **Patrones modernos** de .NET
- ✅ **Mejores prácticas** de la industria
- ✅ **Código mantenible** y escalable
- ✅ **Separación de responsabilidades** cristalina
- ✅ **Testing-friendly** design

Con este trabajo demuestro la implementación de **arquitectura limpia** con **mejores prácticas de la industria**, resultando en un código **mantenible, escalable y robusto** que puede servir como referencia para aplicaciones empresariales modernas y como muestra de mis capacidades tecnicas.

---

## 🧪🚀 Lo que me hubiera gustado implementar (y dejé fuera por tiempo)

Para no dilatar el proceso, prioricé entregar una base sólida. Aun así, hay mejoras que me hubiera gustado incluir si tenía más tiempo:

- Cache con **Redis** (StackExchange.Redis): cachear listados y detalles con claves siguiendo practicas como TTL, good key naming y demas cosas que he aprendido sobre caching.
- **Pruebas de integración** tenia la intencion de implementarlas con test containers, para practicar tambien estas habilidades, pero lo termine descartando.

*Desarrollado con ❤️ y las mejores prácticas de la industria*
