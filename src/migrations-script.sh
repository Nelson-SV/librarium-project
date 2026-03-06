#!/bin/bash

dotnet ef migrations add ChangedStatusFieldInLoanToNonNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations