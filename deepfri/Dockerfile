FROM golang:1.12.9

WORKDIR /deepfri

COPY . .

RUN ["go", "get", "github.com/githubnemo/CompileDaemon"]

ENTRYPOINT CompileDaemon -log-prefix=false -build="go build ./cmd/api/" -command="./api"

CMD ["/bin/bash"]