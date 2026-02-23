#!/bin/bash

dotnet ef migrations add AddLoanForeignKeys \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations