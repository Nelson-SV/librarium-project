#!/bin/bash

dotnet ef migrations script 20260305215306_AddBookAuthorNavigationInBook \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V005__add_member_email_unique_index.sql