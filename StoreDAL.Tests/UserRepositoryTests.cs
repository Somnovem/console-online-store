using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

namespace StoreDAL.Tests;

public class UserRepositoryTests : IDisposable
{
    private readonly StoreDbContext context;
    private readonly IUserRepository repository;

    public UserRepositoryTests()
    {
        var factory = new StoreDbFactory(new TestDataFactory());
        context = factory.CreateContext();
        this.repository = new UserRepository(context);
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldCreateInstance()
    {
        // Arrange & Act
        var repo = new UserRepository(context);

        // Assert
        Assert.NotNull(repo);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
    }

    [Fact]
    public void Add_WithValidUser_ShouldAddUserToDatabase()
    {
        // Arrange
        var hashedPassword = TestDataFactory.HashPassword("testpass");
        var newUser = new User(100, "Test", "User", "test.user", hashedPassword, 2);
        var initialCount = context.Users.Count();

        // Act
        repository.Add(newUser);

        // Assert
        var actualCount = context.Users.Count();
        Assert.Equal(initialCount + 1, actualCount);

        var addedUser = context.Users.Find(100);
        Assert.NotNull(addedUser);
        Assert.Equal("Test", addedUser.Name);
        Assert.Equal("User", addedUser.LastName);
        Assert.Equal("test.user", addedUser.Login);
        Assert.Equal(2, addedUser.RoleId);
    }

    [Fact]
    public void Add_WithNullUser_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Add(null));
    }

    [Fact]
    public void GetAll_ShouldReturnAllUsers()
    {
        // Act
        var users = repository.GetAll();

        // Assert
        Assert.NotNull(users);
        Assert.Equal(7, users.Count()); // Based on test data factory
    }

    [Fact]
    public void GetAll_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        int pageNumber = 2;
        int rowCount = 3;

        // Act
        var users = repository.GetAll(pageNumber, rowCount);

        // Assert
        Assert.NotNull(users);
        var enumerable = users as User[] ?? users.ToArray();
        Assert.Equal(3, enumerable.Count());
            
        // Should skip first 3 users and take next 3
        var userList = enumerable.ToList();
        Assert.Equal(4, userList[0].Id); // 4th user should be first in page 2
    }

    [Fact]
    public void GetAll_WithInvalidPageNumber_ShouldDefaultToPage1()
    {
        // Arrange
        int invalidPageNumber = 0;
        int rowCount = 2;

        // Act
        var users = repository.GetAll(invalidPageNumber, rowCount);

        // Assert
        Assert.NotNull(users);
        var enumerable = users as User[] ?? users.ToArray();
        Assert.Equal(2, enumerable.Count());
            
        var userList = enumerable.ToList();
        Assert.Equal(1, userList[0].Id); // Should start from first user
    }

    [Fact]
    public void GetAll_WithInvalidRowCount_ShouldDefaultTo10()
    {
        // Arrange
        int pageNumber = 1;
        int invalidRowCount = -1;

        // Act
        var users = repository.GetAll(pageNumber, invalidRowCount);

        // Assert
        Assert.NotNull(users);
        Assert.Equal(7, users.Count()); // All users since we have only 7 total
    }

    [Fact]
    public void GetById_WithValidId_ShouldReturnUser()
    {
        // Arrange
        int userId = 2; // Alice from test data

        // Act
        var user = repository.GetById(userId);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
        Assert.Equal("Alice", user.Name);
        Assert.Equal("Johnson", user.LastName);
        Assert.Equal("alice.j", user.Login);
        Assert.Equal(2, user.RoleId); // Registered role
    }

    [Fact]
    public void GetById_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        int invalidId = 999;

        // Act
        var user = repository.GetById(invalidId);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public void Update_WithValidUser_ShouldUpdateUserInDatabase()
    {
        // Arrange
        var existingUser = context.Users.Find(3);
        Assert.NotNull(existingUser);
            
        var updatedUser = new User(3, "Robert", "Wilson Jr.", "bob.wilson", existingUser.Password, 2);

        // Act
        repository.Update(updatedUser);

        // Assert
        var retrievedUser = context.Users.Find(3);
        Assert.NotNull(retrievedUser);
        Assert.Equal("Robert", retrievedUser.Name);
        Assert.Equal("Wilson Jr.", retrievedUser.LastName);
        Assert.Equal("bob.wilson", retrievedUser.Login);
    }

    [Fact]
    public void Update_WithNullUser_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Update(null));
    }

    [Fact]
    public void Update_WithNonExistentUser_ShouldNotThrow()
    {
        // Arrange
        var hashedPassword = TestDataFactory.HashPassword("password");
        var nonExistentUser = new User(999, "Non", "Existent", "non.existent", hashedPassword, 2);

        // Act & Assert
        // Should not throw exception, just do nothing
        repository.Update(nonExistentUser);
            
        // Verify it wasn't added
        var user = context.Users.Find(999);
        Assert.Null(user);
    }

    [Fact]
    public void Delete_WithValidUser_ShouldRemoveUserFromDatabase()
    {
        // Arrange
        var userToDelete = context.Users.Find(7); // Guest user
        Assert.NotNull(userToDelete);
        var initialCount = context.Users.Count();

        // Act
        repository.Delete(userToDelete);

        // Assert
        var actualCount = context.Users.Count();
        Assert.Equal(initialCount - 1, actualCount);
            
        var deletedUser = context.Users.Find(7);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void Delete_WithNullUser_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Delete(null));
    }

    [Fact]
    public void DeleteById_WithValidId_ShouldRemoveUserFromDatabase()
    {
        // Arrange
        int userIdToDelete = 6; // Emily from test data
        var initialCount = context.Users.Count();

        // Act
        repository.DeleteById(userIdToDelete);

        // Assert
        var actualCount = context.Users.Count();
        Assert.Equal(initialCount - 1, actualCount);
            
        var deletedUser = context.Users.Find(userIdToDelete);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void DeleteById_WithInvalidId_ShouldNotThrowAndNotChangeCount()
    {
        // Arrange
        int invalidId = 999;
        var initialCount = context.Users.Count();

        // Act
        repository.DeleteById(invalidId);

        // Assert
        var actualCount = context.Users.Count();
        Assert.Equal(initialCount, actualCount);
    }

    [Fact]
    public void GetUserByLogin_WithValidLogin_ShouldReturnUserWithRole()
    {
        // Arrange
        string login = "alice.j";

        // Act
        var user = repository.GetUserByLogin(login);

        // Assert
        Assert.NotNull(user);
        Assert.Equal("Alice", user.Name);
        Assert.Equal("Johnson", user.LastName);
        Assert.Equal(login, user.Login);
        Assert.NotNull(user.Role);
        Assert.Equal("Registered", user.Role.RoleName);
        Assert.Equal(2, user.Role.Id);
    }

    [Fact]
    public void GetUserByLogin_WithInvalidLogin_ShouldReturnNull()
    {
        // Arrange
        string invalidLogin = "nonexistent.user";

        // Act
        var user = repository.GetUserByLogin(invalidLogin);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public void GetUserByLogin_WithNullLogin_ShouldReturnNull()
    {
        // Arrange
        string? nullLogin = null;

        // Act
        var user = repository.GetUserByLogin(nullLogin);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public void GetUserByLogin_WithEmptyLogin_ShouldReturnNull()
    {
        // Arrange
        string emptyLogin = "";

        // Act
        var user = repository.GetUserByLogin(emptyLogin);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public void GetUserByLogin_ShouldIncludeRoleData()
    {
        // Arrange
        string adminLogin = "admin";

        // Act
        var user = repository.GetUserByLogin(adminLogin);

        // Assert
        Assert.NotNull(user);
        Assert.NotNull(user.Role);
        Assert.Equal("Admin", user.Role.RoleName);
        Assert.Equal(1, user.Role.Id);
    }

    [Fact]
    public void GetAll_ShouldReturnUsersOrderedById()
    {
        // Act
        var users = repository.GetAll(1, 4);

        // Assert
        var userList = users.ToList();
        Assert.True(userList[0].Id < userList[1].Id);
        Assert.True(userList[1].Id < userList[2].Id);
        Assert.True(userList[2].Id < userList[3].Id);
    }

    [Theory]
    [InlineData("admin", "John", "Smith", "Admin")]
    [InlineData("bob.w", "Bob", "Wilson", "Registered")]
    [InlineData("guest", "Guest", "User", "Guest")]
    public void GetUserByLogin_WithDifferentLogins_ShouldReturnCorrectUserData(string login, string expectedName, string expectedSurname, string expectedRole)
    {
        // Act
        var user = repository.GetUserByLogin(login);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(expectedName, user.Name);
        Assert.Equal(expectedSurname, user.LastName);
        Assert.Equal(login, user.Login);
        Assert.NotNull(user.Role);
        Assert.Equal(expectedRole, user.Role.RoleName);
    }

    [Theory]
    [InlineData(1, 3, 3)] // Page 1, 3 items
    [InlineData(2, 3, 3)] // Page 2, 3 items
    [InlineData(3, 3, 1)] // Page 3, 3 items (only 1 remaining)
    [InlineData(4, 3, 0)] // Page 4, 3 items (no items left)
    public void GetAll_WithDifferentPaginationParameters_ShouldReturnCorrectCount(int pageNumber, int rowCount, int expectedCount)
    {
        // Act
        var users = repository.GetAll(pageNumber, rowCount);

        // Assert
        Assert.Equal(expectedCount, users.Count());
    }

    [Fact]
    public void GetUserByLogin_WithCaseSensitiveLogin_ShouldBeExact()
    {
        // Arrange - Test case sensitivity
        string correctLogin = "alice.j";
        string incorrectLogin = "Alice.J";

        // Act
        var correctUser = repository.GetUserByLogin(correctLogin);
        var incorrectUser = repository.GetUserByLogin(incorrectLogin);

        // Assert
        Assert.NotNull(correctUser);
        Assert.Null(incorrectUser); // Should be case sensitive
    }

    [Fact]
    public void DeleteById_ShouldUseDeleteMethod()
    {
        // This test verifies that DeleteById properly calls the Delete method
        // Arrange
        int userIdToDelete = 5; // Mike from test data
        var userBefore = context.Users.Find(userIdToDelete);
        Assert.NotNull(userBefore);

        // Act
        repository.DeleteById(userIdToDelete);

        // Assert
        var userAfter = context.Users.Find(userIdToDelete);
        Assert.Null(userAfter);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}