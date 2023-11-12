### Add migration
Add-Migration ProductNullableCategory -StartupProject Eshop.API -Context EshopDbContext

### Update database up to specific migration
Update-Database 20231110230329_AddCategories

### Run RabbitMQ docker container
docker run -it --rm --name rabbitmq-masstransit -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management