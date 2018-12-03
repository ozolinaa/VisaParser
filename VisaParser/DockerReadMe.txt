# https://ropenscilabs.github.io/r-docker-tutorial/04-Dockerhub.html
# https://docs.docker.com/docker-cloud/builds/push-images/
--Client
docker login --username=xtonyx
docker tag visaparser xtonyx/visaparser:v4
docker push xtonyx/visaparser:v4

--Server
docker run -d --restart=unless-stopped -e 'utcHourToSendLog=19' -e 'interviewRequired=false' -e 'emails=anton.ozolin@gmail.com irina-kedrova@yandex.ru' --name visaParser xtonyx/visaparser:v1
# docker stop visaParser
# docker rm visaParser
# docker rmi $(docker images -a -q)