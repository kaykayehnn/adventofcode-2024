FROM node:22-alpine

WORKDIR /home/node/app
COPY *.js input.txt ./

ENTRYPOINT [ "node" ]
CMD [ "index.js" ]