1 - Added the initial schema migration in V001__initial_schema.sql.
Created the entities Book, Loan and Member with the required params, 
and also the LibrariumDbContext.

2 - Added a new migration in V002__add_loan_foreign_keys.sql.
Realized I didn't added the foreign keys relation in the Loan table.
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



