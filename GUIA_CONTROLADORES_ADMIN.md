# Nerflix - Guía de Uso de Controladores CRUD para Admin

## 📋 Resumen Completo

Se han creado todos los controladores y servicios necesarios para que el administrador pueda gestionar **Títulos** (películas y series) y **Episodios** en la aplicación Nerflix.

## 🏗️ Arquitectura Implementada

### Capas del Proyecto

```
projectWeb.Domain/
├── Entities/
│   ├── Title.cs (Películas y Series)
│   ├── Episode.cs (Episodios de series)
│   ├── Genre.cs (Géneros)
│   ├── TitleGenre.cs (Relación muchos a muchos)
│   └── TitleType.cs (Enum: Movie, Series, Documentary)

projectWeb.Application/
├── DTOs/
│   ├── Title/
│   │   ├── CreateTitleDto.cs
│   │   ├── UpdateTitleDto.cs
│   │   └── TitleDto.cs
│   └── Episode/
│       ├── CreateEpisodeDto.cs
│       ├── UpdateEpisodeDto.cs
│       └── EpisodeDto.cs
├── Interfaces/
│   ├── ICloudinaryService.cs
│   ├── ITitleService.cs
│   └── IEpisodeService.cs
└── Services/
    ├── Title/TitleService.cs
    └── Episode/EpisodeService.cs

projectWeb.Infrastructure/
├── Services/
│   └── CloudinaryService.cs (Subida de archivos a Cloudinary)
└── Extensions/
    └── ServiceCollectionExtension.cs (Registro de servicios)

projectWeb.Api/
└── Controllers/
    └── AdminControllers/
        ├── TitleController.cs
        └── EpisodeController.cs
```

## ✨ Funcionalidades Implementadas

### 1. **Gestión de Títulos** (Películas y Series)

#### Endpoints Disponibles:

| Método | Endpoint | Descripción | Autorización |
|--------|----------|-------------|--------------|
| GET | `/api/admin/titles` | Obtener todos los títulos | Admin |
| GET | `/api/admin/titles/{id}` | Obtener un título por ID | Admin |
| POST | `/api/admin/titles` | Crear un nuevo título | Admin |
| PUT | `/api/admin/titles/{id}` | Actualizar un título | Admin |
| DELETE | `/api/admin/titles/{id}` | Eliminar un título | Admin |

### 2. **Gestión de Episodios**

#### Endpoints Disponibles:

| Método | Endpoint | Descripción | Autorización |
|--------|----------|-------------|--------------|
| GET | `/api/admin/episodes` | Obtener todos los episodios | Admin |
| GET | `/api/admin/episodes/{id}` | Obtener un episodio por ID | Admin |
| GET | `/api/admin/episodes/by-title/{titleId}` | Obtener episodios de una serie | Admin |
| POST | `/api/admin/episodes` | Crear un nuevo episodio | Admin |
| PUT | `/api/admin/episodes/{id}` | Actualizar un episodio | Admin |
| DELETE | `/api/admin/episodes/{id}` | Eliminar un episodio | Admin |

## 🔐 Configuración de Cloudinary

Asegúrate de configurar tus credenciales de Cloudinary en `appsettings.json`:

```json
{
  "Cloudinary": {
    "CloudName": "TU_CLOUD_NAME_AQUI",
    "ApiKey": "TU_API_KEY_AQUI",
    "ApiSecret": "TU_API_SECRET_AQUI"
  }
}
```

## 📤 Ejemplos de Uso con Postman/Insomnia

### 1. Crear un Título (Película)

**POST** `/api/admin/titles`

**Headers:**
```
Authorization: Bearer {tu_jwt_token}
Content-Type: multipart/form-data
```

**Body (form-data):**
```
TitleName: "El Padrino"
Description: "La historia de una familia mafiosa..."
ReleaseYear: 1972
DurationMinutes: 175
Status: (vacío para películas)
AgeRating: "R"
ImdbRating: 9.2
TotalSeasons: (vacío para películas)
Type: 1  // 1=Movie, 2=Series, 3=Documentary
GenreIds[0]: {guid-del-genero-drama}
GenreIds[1]: {guid-del-genero-crimen}
MovieFile: {archivo-video.mp4}
TrailerFile: {archivo-trailer.mp4}
CoverImage: {imagen-portada.jpg}
BackdropImage: {imagen-fondo.jpg}
```

### 2. Crear un Título (Serie)

**POST** `/api/admin/titles`

**Body (form-data):**
```
TitleName: "Breaking Bad"
Description: "Un profesor de química se convierte en..."
ReleaseYear: 2008
Status: "Ended"
AgeRating: "TV-MA"
ImdbRating: 9.5
TotalSeasons: "5"
Type: 2  // Series
GenreIds[0]: {guid-del-genero-drama}
GenreIds[1]: {guid-del-genero-thriller}
TrailerFile: {archivo-trailer.mp4}
CoverImage: {imagen-portada.jpg}
BackdropImage: {imagen-fondo.jpg}
```

### 3. Crear un Episodio

**POST** `/api/admin/episodes`

**Headers:**
```
Authorization: Bearer {tu_jwt_token}
Content-Type: multipart/form-data
```

**Body (form-data):**
```
TitleId: {guid-de-la-serie}
Title: "Pilot"
Description: "Walter White, un profesor de química..."
SeasonNumber: 1
EpisodeNumber: 1
DurationMinutes: 58
VideoFile: {archivo-episodio.mp4}
ThumbnailImage: {imagen-thumbnail.jpg}
```

### 4. Actualizar un Título

**PUT** `/api/admin/titles/{id}`

**Body (form-data):**
```
TitleName: "El Padrino - Edición Especial"
ImdbRating: 9.3
CoverImage: {nueva-imagen-portada.jpg}
// Solo incluye los campos que quieres actualizar
```

### 5. Obtener Todos los Episodios de una Serie

**GET** `/api/admin/episodes/by-title/{guid-de-la-serie}`

**Headers:**
```
Authorization: Bearer {tu_jwt_token}
```

## 🔧 Próximos Pasos para Completar la Configuración

### 1. Configurar Cloudinary
```bash
# Reemplaza los placeholders en appsettings.json con tus credenciales reales
```

### 2. Registrar los Servicios en el Startup

Si tu proyecto usa un archivo de configuración principal (como `Program.cs` o `Startup.cs`), asegúrate de llamar a los métodos de extensión:

```csharp
// En Program.cs o Startup.cs
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddInfrastructureServices(); // ← Nuevo: Registra CloudinaryService
builder.Services.AddApplicationServices(); // ← Ya incluye Title y Episode services
```

### 3. Verificar las Migraciones de Base de Datos

Asegúrate de que todas las entidades estén configuradas correctamente en el DbContext:

```bash
# Crear una nueva migración si es necesario
dotnet ef migrations add AddTitleAndEpisodeManagement

# Actualizar la base de datos
dotnet ef database update
```

## 📊 Modelo de Datos

### TitleType (Enum)
```csharp
public enum TitleType
{
    Movie = 1,        // Película
    Series = 2,       // Serie
    Documentary = 3   // Documental
}
```

### Relaciones
- **Title** ↔ **Episode**: Un título (serie) puede tener muchos episodios
- **Title** ↔ **Genre**: Relación muchos a muchos a través de TitleGenre
- Los archivos (videos e imágenes) se almacenan en Cloudinary y solo guardamos las URLs

## 🚀 Características Principales

1. **Subida de Archivos**: Integración completa con Cloudinary para videos e imágenes
2. **Autorización**: Solo usuarios con rol "Admin" pueden acceder
3. **Validación**: Validación de tipos de títulos y existencia de recursos
4. **Separación de Responsabilidades**: DTOs separados para creación, actualización y lectura
5. **Manejo de Errores**: Excepciones personalizadas para recursos no encontrados

## ⚠️ Consideraciones Importantes

1. **Tamaño de Archivos**: Cloudinary tiene límites en su plan gratuito. Videos muy grandes pueden requerir un plan de pago.

2. **Tiempo de Subida**: La subida de videos puede tomar tiempo. Considera implementar procesamiento asíncrono para archivos grandes.

3. **Gestión de Géneros**: Por ahora, la actualización de géneros es básica. Para producción, deberías implementar una lógica completa de sincronización.

4. **Eliminación de Archivos**: El método `DeleteFileAsync` está implementado pero no se llama automáticamente al actualizar archivos. Considera implementar esta lógica para evitar archivos huérfanos en Cloudinary.

## 📝 Notas Adicionales

- Todos los controladores están protegidos con `[Authorize(Roles = "Admin")]`
- Los archivos se organizan en Cloudinary bajo las carpetas `nerflix/videos` y `nerflix/images`
- Las respuestas incluyen información completa con géneros y tipo de título
- Los DTOs aceptan `multipart/form-data` para permitir la subida de archivos

## 🎯 Resumen de lo Creado

### Archivos Nuevos (16 archivos):

**DTOs:**
1. CreateTitleDto.cs
2. UpdateTitleDto.cs
3. TitleDto.cs
4. CreateEpisodeDto.cs
5. UpdateEpisodeDto.cs
6. EpisodeDto.cs

**Interfaces:**
7. ITitleService.cs
8. IEpisodeService.cs

**Servicios:**
9. TitleService.cs
10. EpisodeService.cs
11. CloudinaryService.cs

**Controladores:**
12. TitleController.cs
13. EpisodeController.cs

**Archivos Actualizados:**
14. ApplicationServiceCollectionExtension.cs (registro de servicios)
15. ServiceCollectionExtension.cs (registro de infraestructura)

---

¡Todo está listo para que el admin pueda gestionar videos en Nerflix! 🎬🍿
