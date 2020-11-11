FROM node:alpine

WORKDIR /front

COPY package.json /front

RUN npm install

RUN npm update

COPY . /front

CMD ["npm", "run", "start"]