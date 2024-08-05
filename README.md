**Overview**

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
   - Users
     ![image](https://github.com/user-attachments/assets/46879912-8a3d-4237-a897-92f67ce7604a)

   - LoanApplications
     ![image](https://github.com/user-attachments/assets/bb9a1797-4e06-445a-b947-102e0cf6ee66)

   - Payments
     ![image](https://github.com/user-attachments/assets/ca17fccc-9bf5-4edc-9541-d026cd498b5a)
     
    - **Ignore is IsSettled column**

4. Controller are covered by Unit Tests
   - NUnit framework is used for adding unit tests.
   - Full coverage of the project is not there due to shortage of time.
   - Moq is used for handling the data operation in tests.

 
**Rest end point Collection attached**


[AspireSmallFinance_Collection.zip](https://github.com/user-attachments/files/16487683/AspireSmallFinance_Collection.zip)


**Process Flow**
1. User authorize by adding Username and Password. (Password is welcome@1 for all users).
2. Admin user (WayneB) can see the current loan applications in the system.
   - Admin can approve the pending loan application
   - Admin can see the loan application detail and payment plan
3. Non-Admin user will be able see only there loan applications.
   - User can see the details and payment plans with history of payments.
   - User can submit payment which will mark the due payment as settled.
   - In case of extra payment, API will automatically adjust the future payments keeping the tenor same.
   - In case of full payment, API will mark all the payments as settles and the loan as **Closed**.
    


**Contact**

For any questions or issues, feel free contact on singh.bhupindertomar@gmail.com.
