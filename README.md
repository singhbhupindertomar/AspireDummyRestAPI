![image](https://github.com/user-attachments/assets/82670154-5654-4461-9f65-7688886b9a4f)**Overview**

This is a RESTful API designed for Aspire Mini Loan Application. The API provides endpoints to interact with Aspire Loan App for Admin as well as Non-Admin user.

**Prerequisites**

- Visual Studio 2022
- Postman/Swagger
  
**Installation**

Clone the repository:
- git clone https://github.com/singhbhupindertomar/AspireDummyRestAPI.git 

![image](https://github.com/user-attachments/assets/1ec0b7a5-c4e3-4f46-b9e4-1c66704268be)

**Summary**

1. API support operations in 3 groups

- Authentication
  ![image](https://github.com/user-attachments/assets/8a885042-5538-4b0f-b133-3b87eeb3fb2e)
  
- Admin operations
![image](https://github.com/user-attachments/assets/b6372677-b82a-4621-a8d9-c72497b4e5eb)

- Customer operations
  ![image](https://github.com/user-attachments/assets/7e0d5a0d-3585-4697-bf07-7af82ab12227)

2. API uses SQL as database which is hosted on AWS.
   - 3 Database tables are added as of now
   - **Users**
   - 
     ![image](https://github.com/user-attachments/assets/46879912-8a3d-4237-a897-92f67ce7604a)

   - **LoanApplications**
   - 
     ![image](https://github.com/user-attachments/assets/bb9a1797-4e06-445a-b947-102e0cf6ee66)

   - **Payments**
   - 
     ![image](https://github.com/user-attachments/assets/ca17fccc-9bf5-4edc-9541-d026cd498b5a)
     
    - **Ignore is IsSettled column**

4. Controller are covered by Unit Tests
   - **NUnit framework** is used for adding unit tests.
   - Full coverage of the project is not there due to shortage of time.
   - **Moq** is used for handling the data operation in tests.

 
**Rest end point Collection attached**


[AspireSmallFinance_Collection.zip](https://github.com/user-attachments/files/16487683/AspireSmallFinance_Collection.zip)


**Process Flow**
1. The user is authorized by adding a username and password. API has basic authentication. (Password is welcome@1 for all users).
   
   ![image](https://github.com/user-attachments/assets/93e6eb64-543e-4e8c-bb0b-6092c04bc069)


2. Admin user (WayneB)
   - Admin users can see the current loan applications in the system.
   
    ![image](https://github.com/user-attachments/assets/4c7e35bf-ce0d-447a-9fdd-75713740fd5b)

   - Admin can approve the pending loan application
  
     ![image](https://github.com/user-attachments/assets/877e343c-3008-4006-a830-6811bf3aac9e)

    
   - Admin can see the loan application details and payment plan

     ![image](https://github.com/user-attachments/assets/51846405-6bd3-4d6a-bffe-653095ea91ce)

4. Non-Admin
   - User can create/submit a new loan application

     ![image](https://github.com/user-attachments/assets/df5bc0f8-8aed-49af-a028-ac87d2ac4a1d)

   - Users will only be able to see their loan applications.

     ![image](https://github.com/user-attachments/assets/df77e856-5f7c-433d-bbec-61ce3c9b1519)

   - The User can see the details and payment plans with the history of payments.

     ![image](https://github.com/user-attachments/assets/36c2b69f-5cee-44b9-86cf-e914acbd37f3)

   - The user can submit payment marking the due payment as settled.
     
     ![image](https://github.com/user-attachments/assets/c6ee80da-6ff0-4413-b003-402c023cc1e5)

   - In case of extra payment, API will automatically adjust the future payments keeping the tenor the same.
   - For the full payment scenario, API will mark all the payments as settled and the loan as **Closed**.
    


**Contact**

For any questions or issues, feel free to contact on singh.bhupindertomar@gmail.com.
