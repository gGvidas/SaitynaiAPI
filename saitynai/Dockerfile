FROM node:alpine

WORKDIR /front

COPY package.json /front

RUN npm install

COPY . /front

CMD ["npm", "run", "start"]