﻿namespace LibraryManagement.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly LibraryManagementDbContext _context;

        public InitialHostDbBuilder(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new DefaultStatusCreator(_context).Create();
            new DefaultTaskTypeCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
