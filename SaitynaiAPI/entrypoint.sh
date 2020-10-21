#!/bin/bash

set -e
run_cmd="dotnet run"

until dotnet-ef database update; do
>&2 echo "MySQL is starting up"
sleep 1
done

>&2 echo "MySQL is up - executing command"
exec $run_cmd