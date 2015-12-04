# WebApiRedisProvider
Sample of use the Redis Memory Cache with WebApi

### Introduction
This project is a sample of server-side WebApi application with Redis and EntityFramework.

### Project structure
The project has a only one branch, named as master.

#### Redis
The Controller `StudentsController` has the sample of the Redis implementation:
 - This controller can be use two classes (`StudentRedisRepository` and `RedisCacheManager`)

#### EntityFramework
The Controller `StudentsEFController` has the sample of the EF implementation:
 - This controller use the class `StudentRedisRepository`

### Testing
This solution contain a test project. Test project use MSTest runner. Open the project, build it, and then test cases should appear in test explorer. Run all the tests in the test explorer.
