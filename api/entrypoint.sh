#!/bin/bash

set -e
run_cmd="dotnet run --urls $ASPNETCORE_URLS --environment $ASPNETCORE_ENVIRONMENT"

exec $run_cmd

