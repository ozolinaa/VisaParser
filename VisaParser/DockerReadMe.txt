# https://ropenscilabs.github.io/r-docker-tutorial/04-Dockerhub.html
# https://docs.docker.com/docker-cloud/builds/push-images/
--Client
docker login --username=xtonyx
docker tag visaparser xtonyx/visaparser:v3
docker push xtonyx/visaparser:v3

--Server
docker run -d --restart=unless-stopped -e 'utcHourToSendLog=6' -e 'interviewRequired=true' -e 'emails=anton.ozolin@gmail.com azhmurkova@gmail.com angubenko@gmail.com' --name visaParser xtonyx/visaparser:v3
# docker stop visaParser
# docker rm visaParser
# docker rmi $(docker images -a -q)