
run mongo db
docker run -d --rm -p 27017:27017 --name mongo -v mongodbdata:/data/db mongo:latest

run app
docker-compose up