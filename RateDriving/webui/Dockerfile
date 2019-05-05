FROM node:8.15.1-jessie
WORKDIR /usr/src/app
COPY package.json yarn.lock ./
RUN yarn
COPY . ./
EXPOSE 3000
CMD ["yarn", "build"]
CMD ["yarn", "start"]