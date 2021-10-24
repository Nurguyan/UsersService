# Описание проектов
## UsersService
Сервер: .NET5 приложение
- Реализует клиентские методы для работы с пользователями: GetUsers(), GetUser(), UpdateUser(), DeleteUser();
- Даннные о пользователях хранятся в MS Sql Server;
- Взаимодействует с клиентами по gRPC.

## ClientMVC
Клиент: .NET5 ASP.NET Core MVC
- Взаимодействует с сервером по gRPC;
- Реализация WebUI: cерверный рендеринг (Razor Views).

## ReactClient
Клиент: .NET5 ASP.NET Core WebAPI + React
- Взаимодействует с сервером по gRPC;
- Реализация WebUI: клиентский рендеринг (React);
- Предоставляет RESTful API клиенту;
- Автоматическое обновление списка пользователей через поллинг.
