# https://ropenscilabs.github.io/r-docker-tutorial/04-Dockerhub.html
# https://docs.docker.com/docker-cloud/builds/push-images/
--Client
docker login --username=xtonyx
docker tag visaparser xtonyx/visaparser:v2
docker push xtonyx/visaparser:v2

--Server
docker stop visaParser
docker rm visaParser
docker run -d --restart=unless-stopped --name visaParser xtonyx/visaparser
