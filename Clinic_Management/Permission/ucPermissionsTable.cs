using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Permission
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Security;
    using System.Security.Cryptography;
    using System.Windows.Forms;



    // Assume you already have these models/services in your project:
    // PermissionService.GetAll() -> IEnumerable<Permission>
    // RolePermissionService.GetByRoleID(int roleId) -> IEnumerable<Permission> OR IEnumerable<int>
    // RolePermissionService.Add(int roleId, int permissionId)
    // RolePermissionService.Remove(int roleId, int permissionId)
    // UserPermissionOverrideService.GetByUserID(int userId) -> IEnumerable<Permission> OR IEnumerable<int>
    // UserPermissionOverrideService.Add(int userId, int permissionId)   // grant
    // UserPermissionOverrideService.Remove(int userId, int permissionId) // remove grant/override

    public partial class ucPermissionsTable : UserControl
    {


        RolePermissionService rolePermissionService = new RolePermissionService();
        UserPermissionOverrideService userPermissionOverride
            = new UserPermissionOverrideService();
        PermissionService permissionService = new PermissionService();

        // =========================
        // MODE
        // =========================
        private enum enOwnerMode
        {
            None = 0,
            Role = 1,
            User = 2,
            Invalid = 3
        }

        private enOwnerMode _mode = enOwnerMode.None;

        // =========================
        // STATE
        // =========================
        private int? _roleId;
        private int? _userId;

        private List<Permission> _allPermissions = new();
        private HashSet<int> _ownedPermissionIds = new();     // what is currently granted/owned
        private HashSet<int> _originalOwnedPermissionIds = new(); // snapshot for dirty tracking

        private bool _gridReady = false;
        private bool _isDirty = false;

        // =========================
        // EVENTS
        // =========================
        public event Action<bool>? DirtyChanged;

        // =========================
        // CTOR
        // =========================
        public ucPermissionsTable()
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;


            // If you created dgvPermission in designer:
            // - set: AllowUserToAddRows=false, ReadOnly=false
            // - SelectionMode=FullRowSelect
            // - AutoGenerateColumns=false
            // - MultiSelect=false

            BuildGrid();
            WireGridEvents();
            SetEnabledState(false);
        }

        // =========================
        // PUBLIC "GUID" FUNCTIONS
        // =========================
        public void LoadForRole(int roleId)
        {
            _roleId = roleId;
            _userId = null;
            ResolveMode();
            Reload();
        }

        public void LoadForUser(int userId)
        {
            _userId = userId;
            _roleId = null;
            ResolveMode();
            Reload();
        }

        public void ClearContext()
        {
            _roleId = null;
            _userId = null;
            ResolveMode();
            ClearGrid();
        }

        // =========================
        // SAVE
        // =========================
        public bool SaveChanges()
        {
            if (_mode == enOwnerMode.Invalid || _mode == enOwnerMode.None)
                return false;

            if (!_gridReady)
                return false;

            dgvPermission.EndEdit();

            // Build "new selection" from grid
            var newSelected = new HashSet<int>();

            foreach (DataGridViewRow row in dgvPermission.Rows)
            {
                if (row.IsNewRow) continue;

                int permissionId = Convert.ToInt32(row.Cells[colPermissionId.Name].Value);
                bool isChecked = Convert.ToBoolean(row.Cells[colGranted.Name].Value);

                if (isChecked)
                    newSelected.Add(permissionId);
            }

            // Compute diffs vs original snapshot (not vs current _owned)
            // So save is stable even if you call it multiple times.
            var toAdd = newSelected.Except(_originalOwnedPermissionIds).ToList();
            var toRemove = _originalOwnedPermissionIds.Except(newSelected).ToList();

            try
            {
                if (_mode == enOwnerMode.Role)
                {
                    int roleId = _roleId!.Value;

                    foreach (var pid in toAdd)
                        rolePermissionService.SetPermission(
                            new RolePermission { RoleId = roleId,
                                PermissionId = pid,
                                CreatedAt= DateTime.Now 
                            ,IsGranted = true});

                    foreach (var pid in toRemove)
                        rolePermissionService.
                            Revoke(roleId, pid);
                }
                else if (_mode == enOwnerMode.User)
                {
                    int userId = _userId!.Value;

                    foreach (var pid in toAdd)
                        userPermissionOverride.SetPermission(
                            new UserPermissionOverride
                            { UserId = userId, PermissionId = pid
                            ,CreatedAt = DateTime.Now,
                                OverrideType = 1});

                    foreach (var pid in toRemove)
                        userPermissionOverride.Delete(userId, pid);
                }

                // Refresh snapshot to match what we saved
                _originalOwnedPermissionIds = newSelected;
                _ownedPermissionIds = newSelected;

                SetDirty(false);
                return true;
            }
            catch (Exception ex)
            {
                clsMessage.ShowError($"Failed to save permissions.\n{ex.Message}");
                return false;
            }
        }

        // =========================
        // LOAD / RELOAD
        // =========================
        public void Reload()
        {
            ResolveMode();

            if (_mode == enOwnerMode.Invalid || _mode == enOwnerMode.None)
            {
                SetEnabledState(false);
                ClearGrid();
                return;
            }

            SetEnabledState(true);

            LoadAllPermissions();
            LoadOwnedPermissions();

            BindGrid();
            SetDirty(false);
        }

        // =========================
        // INTERNAL: MODE
        // =========================
        private void ResolveMode()
        {
            if (_roleId.HasValue && _userId.HasValue)
            {
                _mode = enOwnerMode.Invalid;
                SetEnabledState(false);
                return;
            }

            if (_roleId.HasValue)
                _mode = enOwnerMode.Role;
            else if (_userId.HasValue)
                _mode = enOwnerMode.User;
            else
                _mode = enOwnerMode.None;
        }

        // =========================
        // INTERNAL: GRID SETUP
        // =========================
        // Create columns once (strong, predictable grid)
        private DataGridViewCheckBoxColumn colGranted = null!;
        private DataGridViewTextBoxColumn colPermissionId = null!;
        private DataGridViewTextBoxColumn colCode = null!;
        private DataGridViewTextBoxColumn colName = null!;
        private DataGridViewTextBoxColumn colModule = null!;
        private DataGridViewTextBoxColumn colDescription = null!;
        private DataGridViewCheckBoxColumn colIsActive = null!;

        private void BuildGrid()
        {
            dgvPermission.AutoGenerateColumns = false;
            dgvPermission.Columns.Clear();

            colGranted = new DataGridViewCheckBoxColumn
            {
                Name = "colGranted",
                HeaderText = "Granted",
                DataPropertyName = "Granted",
                Width = 70
            };

            colPermissionId = new DataGridViewTextBoxColumn
            {
                Name = "colPermissionId",
                HeaderText = "ID",
                DataPropertyName = "PermissionId",
                Width = 60,
                ReadOnly = true
            };

            colCode = new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Code",
                DataPropertyName = "Code",
                Width = 140,
                ReadOnly = true
            };

            colName = new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Name",
                DataPropertyName = "Name",
                Width = 160,
                ReadOnly = true
            };

            colModule = new DataGridViewTextBoxColumn
            {
                Name = "colModule",
                HeaderText = "Module",
                DataPropertyName = "Module",
                Width = 120,
                ReadOnly = true
            };

            colDescription = new DataGridViewTextBoxColumn
            {
                Name = "colDescription",
                HeaderText = "Description",
                DataPropertyName = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };

            colIsActive = new DataGridViewCheckBoxColumn
            {
                Name = "colIsActive",
                HeaderText = "Active",
                DataPropertyName = "IsActive",
                Width = 60,
                ReadOnly = true
            };

            dgvPermission.Columns.AddRange(new DataGridViewColumn[]
            {
            colGranted,
            colPermissionId,
            colCode,
            colName,
            colModule,
            colDescription,
            colIsActive
            });

            dgvPermission.AllowUserToAddRows = false;
            dgvPermission.AllowUserToDeleteRows = false;
            dgvPermission.MultiSelect = false;
            dgvPermission.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            _gridReady = true;
        }

        private void WireGridEvents()
        {
            dgvPermission.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvPermission.IsCurrentCellDirty)
                    dgvPermission.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            dgvPermission.CellValueChanged += (s, e) =>
            {
                if (!_gridReady) return;
                if (e.RowIndex < 0) return;
                if (dgvPermission.Columns[e.ColumnIndex].Name != colGranted.Name) return;

                SetDirty(IsGridDifferentFromOriginal());
                UpdateRecordNumbers();
            };
        }


        private void UpdateRecordNumbers()
        {
            if (dgvPermission.DataSource == null)
            {
                lblRecordNumbers.Text = "Records: 0 | Granted: 0";
                return;
            }

            int total = dgvPermission.Rows.Count;
            int granted = 0;

            foreach (DataGridViewRow row in dgvPermission.Rows)
            {
                if (row.IsNewRow) continue;

                bool isChecked = Convert.ToBoolean(row.Cells[colGranted.Name].Value);
                if (isChecked)
                    granted++;
            }

            lblRecordNumbers.Text = $"Records: {total} | Granted: {granted}";
        }


        // =========================
        // INTERNAL: LOAD DATA
        // =========================
        private void LoadAllPermissions()
        {
            try
            {
                _allPermissions = permissionService.GetAll().Value
                    .OrderBy(p => p.Module)
                    .ThenBy(p => p.Code)
                    .ToList();
            }
            catch (Exception ex)
            {
                _allPermissions = new();
                clsMessage.ShowError($"Failed to load permissions.\n{ex.Message}");
            }
        }

        private void LoadOwnedPermissions()
        {
            _ownedPermissionIds.Clear();

            try
            {
                if (_mode == enOwnerMode.Role)
                {
                    // Option A: service returns permissions
                    var owned = rolePermissionService.GetByRoleId(_roleId!.Value).Value;
                    _ownedPermissionIds = new HashSet<int>(owned.Select(p => p.PermissionId));

                    // Option B (if service returns ids):
                    // var ids = RolePermissionService.GetPermissionIdsByRoleID(_roleId!.Value);
                    // _ownedPermissionIds = new HashSet<int>(ids);
                }
                else if (_mode == enOwnerMode.User)
                {
                    var owned = userPermissionOverride.GetByUserId(_userId!.Value).Value;
                    _ownedPermissionIds = new HashSet<int>(owned.Select(p => p.PermissionId));

                    // If your override table supports DENY/GRANT:
                    // make GetByUserID return only GRANTED permissions,
                    // or filter here (OverrideType == Grant).
                }

                _originalOwnedPermissionIds = new HashSet<int>(_ownedPermissionIds);
            }
            catch (Exception ex)
            {
                _ownedPermissionIds = new HashSet<int>();
                _originalOwnedPermissionIds = new HashSet<int>();
                clsMessage.ShowError($"Failed to load owned permissions.\n{ex.Message}");
            }
        }

        // =========================
        // INTERNAL: BIND
        // =========================
        private class PermissionRowVM
        {
            public bool Granted { get; set; }
            public int PermissionId { get; set; }
            public string Code { get; set; } = "";
            public string Name { get; set; } = "";
            public string Module { get; set; } = "";
            public string Description { get; set; } = "";
            public bool IsActive { get; set; }
        }

        private void BindGrid()
        {
            var list = _allPermissions.Select(p => new PermissionRowVM
            {
                PermissionId = p.PermissionId,
                Code = p.Code,
                Name = p.Name,
                Module = p.Module,
                Description = p.Description,
                IsActive = p.IsActive,
                Granted = _ownedPermissionIds.Contains(p.PermissionId)
            }).ToList();

            dgvPermission.DataSource = new BindingList<PermissionRowVM>(list);
            UpdateRecordNumbers();
        }

        private void ClearGrid()
        {
            dgvPermission.DataSource = null;
            _allPermissions.Clear();
            _ownedPermissionIds.Clear();
            _originalOwnedPermissionIds.Clear();
            SetDirty(false);
            UpdateRecordNumbers();
        }

        // =========================
        // INTERNAL: DIRTY
        // =========================
        private bool IsGridDifferentFromOriginal()
        {
            dgvPermission.EndEdit();

            var current = new HashSet<int>();

            foreach (DataGridViewRow row in dgvPermission.Rows)
            {
                if (row.IsNewRow) continue;

                int permissionId = Convert.ToInt32(row.Cells[colPermissionId.Name].Value);
                bool isChecked = Convert.ToBoolean(row.Cells[colGranted.Name].Value);

                if (isChecked)
                    current.Add(permissionId);
            }

            return !current.SetEquals(_originalOwnedPermissionIds);
        }

        private void SetDirty(bool value)
        {
            if (_isDirty == value) return;
            _isDirty = value;
            DirtyChanged?.Invoke(_isDirty);
        }

        // =========================
        // INTERNAL: ENABLE/DISABLE
        // =========================
        private void SetEnabledState(bool enabled)
        {
            this.Enabled = enabled;

            // If you have a label for state or a groupbox, handle it here:
            // lblState.Text = enabled ? "" : "Select Role OR User (not both)";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if(!SaveChanges())
            {
                clsMessage.ShowError("Something went wrong !,Save Doesn't change.");
                return;
            }


            clsMessage.ShowSuccess("Changes Saved Successfuly!");
            

        }
    }




}
