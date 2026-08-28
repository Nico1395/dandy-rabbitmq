echo "Removing and running compose script... "
docker compose down -v
docker compose build
docker compose up -d
echo "...running!"

echo "Clustering RabbitMQ nodes..."
sleep 4s
docker exec rabbitmq-2 rabbitmqctl stop_app
docker exec rabbitmq-2 rabbitmqctl reset
docker exec rabbitmq-2 rabbitmqctl join_cluster dandy-rabbitmq@rabbitmq-1
docker exec rabbitmq-2 rabbitmqctl start_app
docker exec rabbitmq-2 rabbitmqctl cluster_status
echo "...clustered!"