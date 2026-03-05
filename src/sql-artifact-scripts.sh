#!/bin/bash

dotnet ef migrations script 20260305215306_AddBookAuthorNavigationInBook \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V004__add_bookauthor_nativation_in_book.sql