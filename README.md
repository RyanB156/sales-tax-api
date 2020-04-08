# sales-tax-api
A RESTful API for calculating sales tax for a retail transaction in North Carolina

Endpoint: /salestax/
Supported request methods: GET, POST

##  GET syntax:    
```
  <host>/salestax/?county=<county name>&price=<price>
```
  or  
```
  <host>/salestax/?price=<price>&county=<county name>
```
  
  The order of the query arguments does not matter.
  
## POST syntax:    
```
  <host>/salestax/
  {
    "County": "<county name>",
    "Price": "<price>"
  }
```
  The body must be encoded in JSON format and the request ContentType must reflect this.
  County expects a string and Price expects a number.

---------------------------------------------------------------------------------------------------
## Input Format:

  #### -County  
  
  The county name must have the name of the county followed by "county".
  The county name is case insensitive and allows single spaces or underscores for separating words.

  The following is a nonexhaustive list of possible values for Macon County:  
    ["Macon County", "macon county", "MACON COUNTY", "Macon_County"]
    
  #### -Price
  
  The price must be a positive rational number.
  Negative numbers and zero are not allowed.
  
---------------------------------------------------------------------------------------------------
## Output Format:
    
  ###  Successful Request
  
  The API will return HTTP status code 200 (OK) for successful requests and the following JSON object:
    ```
      {
        "County": <county name>,
        "SalePrice": <price>,
        "TaxRate": <tax rate>,
        "SaleTotal": <total price>
      }
    ```
        
      County is the name of the county that was specified in the request.
      SalePrice is the price that was specified in the request as a decimal.
      TaxRate is the tax rate for the specified county as a decimal.
      SaleTotal is the total price after applying sales tax to the price as a decimal.
    
  ### Failed Request
    
  The API will return HTTP status code 404 (Not Found) for failed GET requests and HTTP status code 400 (Bad Request) 
    for failed POST requests with the following JSON object:
      ```
      {
        "Error": <error message>
      }
      ```
      
   Error is a string containing the error message.
      
   The following errors will be returned by the API:
   ```
      "CountyError" - The specified county did not exist in the database
      "JsonBodyError" - The JSON body was not in the expected format
      "JsonCountyError" - The county field was null in the JSON body
      "JsonContentError" - The POST request did not have a body or the content type did not equal "application/json"
      "LocalPathError" - The local path was not "/salestax/"
      "PriceInvalidError" - The price was <= 0
      "PriceNullError" - Unable to parse the price
      "QueryError" - The GET request was missing the county or price argument
      "UnsupportedMethodError" - The request used an unsupported method type
    ```
