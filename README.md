# API Blog

Este proyecto es una API desarrollada en .NET con Entity Framework Core y PostgreSQL, siguiendo Clean Architecture, CQRS y el Patrón Repository. La API está diseñada para gestionar un blog, permitiendo la creación, modificación y consulta de publicaciones y usuarios.

## 🚀 Tecnologías utilizadas

- **.NET (Web API, Infrastructure, Application, Domain)**
- **Entity Framework Core con PostgreSQL**
- **CQRS (Command and Query Responsibility Segregation)**
- **Patrón Repository**
- **Mensajería con Colas (Implementación básica)**
- **Autenticación centralizada con un sistema externo (API DRF)**
- **Documentación Swagger**

## 📌 Configuración Inicial

### 🔑 Autenticación

La API utiliza un sistema de autenticación centralizado. Para configurar correctamente la autenticación, es necesario convertir la clave de autenticación del backend de autenticación a Base64:

```bash

# Convertir API_AUTH_SECRET_KEY a Base64

$  echo  -n  "TU_SECRET_KEY"  |  base64

```

### Migraciones

Crear migraciones y aplicar las migraciones en la base de datos:

```bash
Add-Migration InitialCreate
Update-Database InitialCreate
```

### 📌 Arquitectura

La API sigue Clean Architecture, con una organización modular:

```bash
api_blog/
│── src/
│   ├── Application/       # Lógica de negocio (CQRS, DTOs, Validaciones)
│   ├── Domain/            # Entidades y reglas de negocio
│   ├── Infrastructure/    # Persistencia de datos (EF Core, Repositorios, Configuración DB)
│   ├── WebAPI/            # Punto de entrada de la API
│── tests/                 # Pruebas unitarias y de integración
│── Worker/			   	   # Worker para la cola de mensajes
```

### 📌 Funcionalidades

Actualmente, la API cuenta con las siguientes funcionalidades.

#### ✅ Gestión de Usuarios

- Crear y actualizar usuarios
- Autenticación centralizada

#### ✅ Gestión de Posts

- Crear, editar y eliminar posts
- Relación con autores y categorías

#### ✅ Mensajería con Colas (En desarrollo)

- Procesamiento asíncrono de eventos
- Sincronización con otros servicios

### 📌 Próximos pasos

- Implementar estadísticas para el blog
- Mejorar varias
