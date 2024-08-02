create migration SQL Server
     dotnet ef migrations add UserTable -s ../Para.Api/ --context ParaDbContext
create migration PostgreSQL Server
     dotnet ef migrations add InitialCreate -s ../Para.Api/ --context ParaDbContext    
  
db guncelleme SQL 
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaDbContext
db guncelleme Postgre
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaDbContext

  REDİS
  -   docker pull redis/redis-stack-server:latest
  -   docker run -d --name redis-stack-server -p 6379:6379 redis/redis-stack-server:latest
  -   docker ps

  RabbitMq
  -   docker pull rabbitmq
  -   docker run -d -p 1453:15672 -p 5672:5672 --name rabbitmqcontainer rabbitmq:3-management