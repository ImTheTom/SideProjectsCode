FROM golang:1.12.4-stretch
ADD . /app
WORKDIR /app
EXPOSE 8080
RUN go get -v github.com/dgrijalva/jwt-go
RUN go get -v github.com/gorilla/mux
RUN cd server && go build
CMD ["./server/server"]