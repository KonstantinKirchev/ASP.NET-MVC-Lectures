using SportSystem.Data.Repositories;
using SportSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SportSystem.Data.UnitOfWork
{
    public class SportSystemData : ISportSystemData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public SportSystemData()
            : this(new SportSystemDbContext())
        {
        }

        public SportSystemData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Bet> Bets
        {
            get
            {
                return this.GetRepository<Bet>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Match> Matches
        {
            get
            {
                return this.GetRepository<Match>();
            }
        }

        public IRepository<Player> Players
        {
            get
            {
                return this.GetRepository<Player>();
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                return this.GetRepository<Team>();
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Vote> Votes
        {
            get
            {
                return this.GetRepository<Vote>();
            }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EntityFrameworkRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
