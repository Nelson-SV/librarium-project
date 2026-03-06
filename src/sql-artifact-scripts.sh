#!/bin/bash

dotnet ef migrations script 20260306140349_AddMemberPhoneNumberAsNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V007__add_member_phone_number_as_non_nullable.sql