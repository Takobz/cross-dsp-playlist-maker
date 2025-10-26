# QUERYING YOUTUBE API USE CASE
This guide attempts to explain the use case of authenticating a user against Google and using their token to get data on their behalf.  

This doc assumes you have your backend api running, please see [guide](../DEVELOPER_GUIDE.md#docker-compose-run) if you haven't configured it yet.

## The Use Case
The use case here is that we need our OAuth application to have access to get data from YouTube.  
For this to happen a user will need to allow our app to get their data on their behalf, via OAuth 2.0.  

The following steps show the REST APIs we will call in the local api and what they do, and what kind of data to expect etc.  

>[!NOTE]
> The REST APIs where tested/called with REST API extension in vs code.  
> If you prefer an alternative like Postman or Bruno (even cURL) please feel free to use them.

1. Assuming the you have configured your app and it is running in docker compose, navigate to: http://localhost:5080/swagger, you should see something similar to this:  
![swagger-endpoints](../../assets/google-use-cases/swagger-endpoints.png)

2. The above just confirms the app is up and we are seeing the expected endpoints.

3. In your REST API tester, call the google initate GET endpoint: http://localhost:5080/auth/google-init, you should get a response like this:  
```json
{
  "data": {
    "authorization_code_flow_redirect": "https://accounts.google.com/o/oauth2/v2/auth?client_id=[YOUR_CLIENT_ID]&redirect_uri=http://localhost:5080/auth/google-callback&response_type=code&scope=https://www.googleapis.com/auth/youtube.force-ssl&access_type=offline&state=44a4343e-9846-471a-b737-c3e8aa8f3125&prompt=select_account"
  },
  "error_messages": []
}
```

4. Just click on the generate link from the json property `authorization_code_flow_redirect`, it should take you to Google's authentication server, see here:
![google-asking-for-consent](../../assets/google-use-cases/google-asking-for-consent.jpeg)

5. Just authorize the request, that will require you to sign into your Google account.

6. After consenting/authorization, google should redirect you to http://localhost:5080/auth/google-callback, you can get the authorization code:
```json
{
  "data": {
    "access_token": "[GOOGLE_GENERATED_ACCESS_TOKEN]",
    "expires_in": 3598,
    "refresh_token": ""
  },
  "error_messages": []
}
```

7. You can take the token you got from google-callback response (the `access_token`) and pass it as a bearer token to the endpoint http://localhost:5080/google/search?query=YOUR_QUERY, seen here:
```json
GET http://localhost:5080/google/search?query=drake
Authorization: Bearer YOUR_GOOGLE_GENERATED_ACCESS_TOKEN_FROM_STEP_6
Accept: application/json

# response here:
{

}
```

This means we were able to get an access token that allowed us to get the authenticated user's data. 