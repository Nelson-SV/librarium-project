#!/bin/bash

dotnet ef migrations add AddIsDeletedFieldWithDefaultValueFalse \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations