#!/bin/bash

dotnet ef database update \
  --project Librarium.Data \
  --startup-project Librarium.Api