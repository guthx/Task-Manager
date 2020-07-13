using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManager.Data.Models
{
    public partial class TaskManagerDBContext : DbContext
    {
        public TaskManagerDBContext()
        {
        }

        public TaskManagerDBContext(DbContextOptions<TaskManagerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersTask> UsersTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.IsViewed).HasColumnName("Is_Viewed");

                entity.Property(e => e.RequestId).HasColumnName("Request_Id");

                entity.Property(e => e.TaskId).HasColumnName("Task_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_notifications_request_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("fk_notifications_task_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_notifications_user_id");
            });

            modelBuilder.Entity<Privilege>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.ReceiverId).HasColumnName("Receiver_Id");

                entity.Property(e => e.SenderId).HasColumnName("Sender_Id");

                entity.Property(e => e.TaskId).HasColumnName("Task_Id");

                entity.Property(e => e.PrivilegeId).HasColumnName("Privilege_Id");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.RequestsReceiver)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_receiver_id");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.RequestsSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_sender_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_task_id");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PrivilegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_privilege_id");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.Property(e => e.ParentId).HasColumnName("Parent_Id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("fk_Tasks_Parent");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__A9D105345A4E708F")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<UsersTask>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TaskId });

                entity.ToTable("Users_Tasks");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.TaskId).HasColumnName("Task_Id");

                entity.Property(e => e.PrivilegeId).HasColumnName("Privilege_Id");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.UsersTasks)
                    .HasForeignKey(d => d.PrivilegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Users_Tasks_Privilege");
            });
        }
    }
}
