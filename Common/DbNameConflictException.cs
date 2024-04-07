public class DbNameConflictException : Exception {
    public DbNameConflictException() {

    }

    public DbNameConflictException(string message) : base(message) {}

    public DbNameConflictException(string message, Exception inner) : base(message, inner) {}
}