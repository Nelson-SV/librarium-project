1 - Added the initial schema migration in V001__initial_schema.sql.
Created the entities Book, Loan and Member with the required params, 
and also the LibrariumDbContext.

2 - Added a new migration in V002__add_loan_foreign_keys.sql.
Realized I didn't add the foreign keys relation in the Loan table.
(For example: public Member Member { get; set; })

3 - Added a new migration in V003__add_authors_and_book_authors.sql.
Created the Author entity and the BookAuthor relation table.
This relation doesn't affect the existing data in the Book table.
With this, we can have:
- multiple authors for a single book.
- a single author can have multiple books.
- a book can exist without an author.

Also created a new version of the BookController (V2).
Now returns information from both books and authors.

4 - Added a new migration in V004__add_bookauthor_nativation_in_book.sql.
Added the navigation property in the Book entity to be able to return the authors information in the BookController_V2.
To accomplish this and to also don't break the v1 controller/endpoint, I created DTO's models to handle the responses from the endpoints.
This didn't have any effect in the actual database, only in the context snapshot.

5 - Added a new migration in V005__add_member_email_unique_index.sql.
Added a new api endpoint to handle updates in members details.
First we have to update the duplicated emails through the endpoint.
Once we don't have duplicated emails, we can add the migration and make the database update.
This way we make sure we can run the migration without any problem, otherwise SQL Server refuses to create a unique index on a column with duplicate values.

6 - Added a new migration V006__add_member_phone_number_as_nullable.sql and V007__add_member_phone_number_as_non_nullable.sql.
First, added the phone number field as nullable in the first (V006) migration.
Updated the existing data to have a phone number (through the update endpoint).
Then applied the NOT NULL constraint (V007).
This avoids a constraint failure that would occur if I tried to enforce NOT NULL on a column with existing rows containing no data.

7 - Added a new migration V008__add_status_field_in_loan_as_nullable.sql and V009__changed_status_field_in_loan_as_non_nullable.sql.
First added status as nullable on loan (V008), to allow existing rows to remain valid during the transition. 
And before running the migration, added a statement in the migration file (Up method) to update the status information of the existing data, with:
```
migrationBuilder.Sql("UPDATE Loans SET Status = 'Returned' WHERE ReturnDate IS NOT NULL");
migrationBuilder.Sql("UPDATE Loans SET Status = 'Active' WHERE ReturnDate IS NULL");
```
If the return date is null, the loan is Active, otherwise, it's Returned.
Created a Dto response for the first controller (v1) to return what the team is expecting. 
Created a new controller and Dto (v2) to provide status information in the response.
Made the second migration (V009) to change the status field to non-nullable, since we already have all the existing data with the status information.

8 - 




