# Описание проекта
Проект выполнен по архитектуре **Clean architecture** (слои Core, Application, Infrastructure и Presentation)
с использованием дизайн паттернов **Domain Driven Design** (Entities, Repositories, Domain/Application Services, DTO's...)
и с  применением приниципов **SOLID**

## Структура проекта
Структура проекта состоит из следующих слоёв;
* Core
    * Entities    
    * Interfaces
    * Specifications
    * ValueObjects
    * Exceptions
* Application    
    * Interfaces    
    * Services
    * Dtos
    * Mapper
    * Exceptions
* Infrastructure
    * Data
    * Repository
    * Services
    * Migrations
    * Logging
    * Exceptions
* Web
    * Controllers
* Tests	

### Запуск проекта
Запустить проект Finflow.Hlopov.Web
открыть браузер по адресу http://localhost:5500/swagger

#### Используемые технологии
* .NET Core 3.1
* ASP.NET Core 3.1
* Entity Framework Core 3.1 (code-first)
* .NET Core Native DI
* AutoMapper
* Swagger

##### Архитектура и паттерны
* Clean Architecture
* Full architecture with responsibility separation of concerns
* SOLID and Clean Code
* Domain Driven Design (Layers and Domain Model Pattern)
* Unit of Work
* Repository and Generic Repository
* Specification Pattern

