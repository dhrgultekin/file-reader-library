using System.Collections.Generic;

namespace FileReaderLibrary
{
    public class RoleBasedSecurityContext
    {
        private readonly Dictionary<UserRole, List<string>> _rolePermissions;

        public RoleBasedSecurityContext()
        {
            // Dictionary that stores the role permissions
            // It maps each UserRole to a list of AllowedFiles
            _rolePermissions = new Dictionary<UserRole, List<string>>();
        }

        public void AddRolePermissions(UserRole role, List<string> allowedFiles)
        {
            // Adding role permissions to the security context
            // It takes a UserRole and a list of allowedFiles as parameters and associates the role with the corresponding allowed files
            _rolePermissions[role] = allowedFiles;
        }

        // Checks if the current user with the given role is allowed to read the specified file
        public bool CanReadFile(string filePath)
        {
            // Retrieve the current user's role
            UserRole currentUserRole = GetCurrentUserRole();

            // Check if the current user's role has permission to read the file
            if (_rolePermissions.TryGetValue(currentUserRole, out List<string> allowedFiles))
            {
                // Check if the file path exists in the allowed files list
                return allowedFiles.Contains(filePath);
            }

            // If the role doesn't have specific permissions defined, deny access
            return false;
        }

        private UserRole GetCurrentUserRole()
        {
            // Return the appropriate role for the current user
            return UserRole.Admin;
        }
    }
}
