# InfoTrack Technical Challenge

This repository contains my solution for the technical challenge provided by InfoTrack.

## API Details

You can interact with the API at the following endpoint:


URL: https://yourserver/api/v1/Booking

Here is an example of a JSON body for a POST request:

```json
{
  "time": "09:20",
  "name": "John"
}
```

# Solution Structure
The solution contains two projects:
API Project: This project implements the booking API. It uses the Repository pattern for creating bookings. All bookings are stored in-memory.
API Test Project: This project contains the tests for the API. The tests are developed using xUnit and Moq. Moq is used for mocking the repository and data.

# Running the Tests
To run the tests, navigate to the root directory of the solution in your terminal and run the following command:
```console
dotnet test
```

# Further Information
For any additional details or queries, feel free to contact me.
