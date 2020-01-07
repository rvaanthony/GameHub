using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameHubAPI.Models.DB
{
    public partial class GameHubContext : DbContext
    {
        public GameHubContext()
        {
        }

        public GameHubContext(DbContextOptions<GameHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblApicallLog> TblApicallLog { get; set; }
        public virtual DbSet<TblBrowser> TblBrowser { get; set; }
        public virtual DbSet<TblBtbactionType> TblBtbactionType { get; set; }
        public virtual DbSet<TblBtbcaseAmount> TblBtbcaseAmount { get; set; }
        public virtual DbSet<TblBtbgame> TblBtbgame { get; set; }
        public virtual DbSet<TblBtbgameBankerOffer> TblBtbgameBankerOffer { get; set; }
        public virtual DbSet<TblBtbgameCase> TblBtbgameCase { get; set; }
        public virtual DbSet<TblBtbgameChosenCase> TblBtbgameChosenCase { get; set; }
        public virtual DbSet<TblBtbgameLog> TblBtbgameLog { get; set; }
        public virtual DbSet<TblBtbgameResult> TblBtbgameResult { get; set; }
        public virtual DbSet<TblBtbresultType> TblBtbresultType { get; set; }
        public virtual DbSet<TblClient> TblClient { get; set; }
        public virtual DbSet<TblDeviceType> TblDeviceType { get; set; }
        public virtual DbSet<TblErrorLog> TblErrorLog { get; set; }
        public virtual DbSet<TblHttpMethodType> TblHttpMethodType { get; set; }
        public virtual DbSet<TblHttpStatusCode> TblHttpStatusCode { get; set; }
        public virtual DbSet<TblLogin> TblLogin { get; set; }
        public virtual DbSet<TblLoginMethod> TblLoginMethod { get; set; }
        public virtual DbSet<TblOs> TblOs { get; set; }
        public virtual DbSet<TblPlayFabToken> TblPlayFabToken { get; set; }
        public virtual DbSet<TblPlayFabUser> TblPlayFabUser { get; set; }
        public virtual DbSet<TblStatus> TblStatus { get; set; }
        public virtual DbSet<TblStatusType> TblStatusType { get; set; }
        public virtual DbSet<TblTableEntity> TblTableEntity { get; set; }
        public virtual DbSet<TblToken> TblToken { get; set; }
        public virtual DbSet<TblTokenType> TblTokenType { get; set; }
        public virtual DbSet<TblTracking> TblTracking { get; set; }
        public virtual DbSet<TblTrackingAction> TblTrackingAction { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TblApicallLog>(entity =>
            {
                entity.ToTable("tblAPICallLog");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.MethodType)
                    .WithMany(p => p.TblApicallLog)
                    .HasForeignKey(d => d.MethodTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAPICallLog_tblHttpMethodType");

                entity.HasOne(d => d.ResponseStatusCode)
                    .WithMany(p => p.TblApicallLog)
                    .HasForeignKey(d => d.ResponseStatusCodeId)
                    .HasConstraintName("FK_tblAPICallLog_tblHttpStatusCode");

                entity.HasOne(d => d.Token)
                    .WithMany(p => p.TblApicallLog)
                    .HasForeignKey(d => d.TokenId)
                    .HasConstraintName("FK_tblAPICallLog_tblToken");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblApicallLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblAPICallLog_tblUser");
            });

            modelBuilder.Entity<TblBrowser>(entity =>
            {
                entity.ToTable("tblBrowser");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasMaxLength(50);
            });

            modelBuilder.Entity<TblBtbactionType>(entity =>
            {
                entity.ToTable("tblBTBActionType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<TblBtbcaseAmount>(entity =>
            {
                entity.ToTable("tblBTBCaseAmount");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TblBtbgame>(entity =>
            {
                entity.ToTable("tblBTBGame");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("GUID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblBtbgame)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGame_tblStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblBtbgame)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGame_tblUser");
            });

            modelBuilder.Entity<TblBtbgameBankerOffer>(entity =>
            {
                entity.ToTable("tblBTBGameBankerOffer");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.TblBtbgameBankerOffer)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameBankerOffer_tblBTBGame");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblBtbgameBankerOffer)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameBankerOffer_tblStatus");
            });

            modelBuilder.Entity<TblBtbgameCase>(entity =>
            {
                entity.ToTable("tblBTBGameCase");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("GUID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CaseAmount)
                    .WithMany(p => p.TblBtbgameCase)
                    .HasForeignKey(d => d.CaseAmountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameCase_tblBTBCaseAmount");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.TblBtbgameCase)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameCase_tblBTBGame");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblBtbgameCase)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameCase_tblStatus");
            });

            modelBuilder.Entity<TblBtbgameChosenCase>(entity =>
            {
                entity.ToTable("tblBTBGameChosenCase");

                entity.HasOne(d => d.GameCase)
                    .WithMany(p => p.TblBtbgameChosenCase)
                    .HasForeignKey(d => d.GameCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameChoosenCase_tblBTBGameCase");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblBtbgameChosenCase)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameChoosenCase_tblStatus");
            });

            modelBuilder.Entity<TblBtbgameLog>(entity =>
            {
                entity.ToTable("tblBTBGameLog");

                entity.Property(e => e.SourceId).HasMaxLength(50);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.TblBtbgameLog)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameLog_tblBTBActionType");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.TblBtbgameLog)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameLog_tblBTBGame");

                entity.HasOne(d => d.TableEntity)
                    .WithMany(p => p.TblBtbgameLog)
                    .HasForeignKey(d => d.TableEntityId)
                    .HasConstraintName("FK_tblBTBGameLog_tblTableEntity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblBtbgameLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameLog_tblUser");
            });

            modelBuilder.Entity<TblBtbgameResult>(entity =>
            {
                entity.ToTable("tblBTBGameResult");

                entity.Property(e => e.SourceId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.TblBtbgameResult)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameResult_tblBTBGame");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.TblBtbgameResult)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameResult_tblBTBResultType");

                entity.HasOne(d => d.TableEntity)
                    .WithMany(p => p.TblBtbgameResult)
                    .HasForeignKey(d => d.TableEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBTBGameResult_tblTableEntity");
            });

            modelBuilder.Entity<TblBtbresultType>(entity =>
            {
                entity.ToTable("tblBTBResultType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblClient>(entity =>
            {
                entity.ToTable("tblClient");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(100);

                entity.Property(e => e.Osid).HasColumnName("OSId");

                entity.Property(e => e.UserAgent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Browser)
                    .WithMany(p => p.TblClient)
                    .HasForeignKey(d => d.BrowserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblClient_tblBrowser");

                entity.HasOne(d => d.DeviceType)
                    .WithMany(p => p.TblClient)
                    .HasForeignKey(d => d.DeviceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblClient_tblDeviceType");

                entity.HasOne(d => d.Os)
                    .WithMany(p => p.TblClient)
                    .HasForeignKey(d => d.Osid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblClient_tblOS");
            });

            modelBuilder.Entity<TblDeviceType>(entity =>
            {
                entity.ToTable("tblDeviceType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblErrorLog>(entity =>
            {
                entity.ToTable("tblErrorLog");

                entity.Property(e => e.Exception).IsRequired();

                entity.Property(e => e.Info).IsRequired();

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblErrorLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblErrorLog_tblUser");
            });

            modelBuilder.Entity<TblHttpMethodType>(entity =>
            {
                entity.ToTable("tblHttpMethodType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<TblHttpStatusCode>(entity =>
            {
                entity.ToTable("tblHttpStatusCode");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblLogin>(entity =>
            {
                entity.ToTable("tblLogin");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblLogin)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLogin_tblClient");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.TblLogin)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLogin_tblLoginMethod");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblLogin)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLogin_tblUser");
            });

            modelBuilder.Entity<TblLoginMethod>(entity =>
            {
                entity.ToTable("tblLoginMethod");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblOs>(entity =>
            {
                entity.ToTable("tblOS");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPlayFabToken>(entity =>
            {
                entity.ToTable("tblPlayFabToken");

                entity.Property(e => e.EntityToken)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.SessionTicket)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.PlayFabUser)
                    .WithMany(p => p.TblPlayFabToken)
                    .HasForeignKey(d => d.PlayFabUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPlayFabToken_tblPlayFabUser");
            });

            modelBuilder.Entity<TblPlayFabUser>(entity =>
            {
                entity.ToTable("tblPlayFabUser");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PlayFabId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(20);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblPlayFabUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPlayFabUser_tblUser");
            });

            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.ToTable("tblStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TblStatus)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatus_tblStatusType");
            });

            modelBuilder.Entity<TblStatusType>(entity =>
            {
                entity.ToTable("tblStatusType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<TblTableEntity>(entity =>
            {
                entity.ToTable("tblTableEntity");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblToken>(entity =>
            {
                entity.ToTable("tblToken");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("GUID")
                    .HasMaxLength(50);

                entity.Property(e => e.RefreshToken).HasMaxLength(2000);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblToken)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblToken_tblStatus");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TblToken)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblToken_tblTokenType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblToken_tblUser");
            });

            modelBuilder.Entity<TblTokenType>(entity =>
            {
                entity.ToTable("tblTokenType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblTracking>(entity =>
            {
                entity.ToTable("tblTracking");

                entity.Property(e => e.NewValue)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.OldValue).HasMaxLength(2000);

                entity.Property(e => e.SourceId).HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblTracking)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_tblTracking_tblClient");

                entity.HasOne(d => d.TableEntity)
                    .WithMany(p => p.TblTracking)
                    .HasForeignKey(d => d.TableEntityId)
                    .HasConstraintName("FK_tblTracking_tblTableEntity");

                entity.HasOne(d => d.TrackingAction)
                    .WithMany(p => p.TblTracking)
                    .HasForeignKey(d => d.TrackingActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTracking_tblTrackingAction");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTracking)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTracking_tblUser");
            });

            modelBuilder.Entity<TblTrackingAction>(entity =>
            {
                entity.ToTable("tblTrackingAction");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("GUID")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblStatus");
            });
        }
    }
}
