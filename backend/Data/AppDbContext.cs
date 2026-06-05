//orm 쓰기위한 기초형식

using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext
: DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    )
    : base(options)
    {

    }

    public DbSet<TodoItem>
    Todos => Set<TodoItem>();
}