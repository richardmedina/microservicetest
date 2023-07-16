#!/bin/bash

BASE_DIR=../
echo "Building user api... from $BASE_DIR..."

sudo docker build -f $BASE_DIR/src/MicroserviceTest.Api.Email/Dockerfile -t ricki9/microservicetest.api.email $BASE_DIR/src


read -p "do you want to publish to image's repository? [y/N]" -n 1 -r
echo    # (optional) move to a new line
if [[ $REPLY =~ ^[Yy]$ ]]
then
	sudo docker push ricki9/microservicetest.api.email
fi


