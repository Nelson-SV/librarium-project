#!/bin/bash

dotnet ef migrations add RemovedIsbnFieldAndRenamedIsbnnewInBook \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations