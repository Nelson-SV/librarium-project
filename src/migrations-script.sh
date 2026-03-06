#!/bin/bash

dotnet ef migrations add AddStatusFieldInLoanAsNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations