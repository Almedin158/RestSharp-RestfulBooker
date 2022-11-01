Based on: https://restful-booker.herokuapp.com/apidoc/index.html

I made three assumptions here:
1. The entire request body is required and the backend service tests the entire request body against a schema
(meaning I do not need to check each and every parameter, test cases for a single parameter are enough)
2. Invalid request body, such as an empty,null,
invalid data type required fields are handled with a 400 status code bad request.
3. Invalid request bodies where all parameters are sent but the parameter value is incorrect are handled with
a 200 status code ok response containting an error message (status code 422 could have sufficed). 
I have added a "reason" and a list of errors
as response parameters, this would be a way i would image a server handling these issues and notifying the client.