1. Tener .net 7.0.1
2. SDK dotnet-ef 7.0.1
3. SQL Server

Las funcionalidades se encuentra en la carpeta Features dentro de Back.Test.Application, ya que ocupo el patron de diseño CQRS

Para iniciar el proyecto, se debe seleccionar el proyecto de inicio único API
![image](https://github.com/KevinM97/backendTestAltiora/assets/58663814/761bedbb-1258-4500-b6ae-4f2300da3b95)


La cadena de conexión se encuentra en API - appsettings.
No es necesario crear una base de datos, ya que el programa lo realiza al ejecutarse la primera vez.


Metodos:
![image](https://github.com/KevinM97/backendTestAltiora/assets/58663814/00c501d9-7ce8-43c1-99ae-2a5909be914b)


Arquitectura de la solución
Clean Arquitecture

![image](https://github.com/KevinM97/backendTestAltiora/assets/58663814/78b30d59-97ea-4daa-83a8-1d6bac33f1c3)


Domain: Se encuentran las entidades de negocio de la aplicación.
Application: Reglas del negocio ( Interfaces, ViewModels, patron de segración de responsabilidades CQRS.
Infrastructure: Se encuentra las interfaces implementadas.
