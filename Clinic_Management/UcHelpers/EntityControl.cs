using Clinic_Management.Helpers;
using Clinic_Management_BLL.CrudInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Authentication;
using System.Text;

namespace Clinic_Management.EntityUc
{
    public abstract class EntityControl<T> : UserControl
    where T : class, new()
    {
        protected T Entity = new();

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EntityId { get; protected set; }


        protected BaseCrudService<T> Service;

        protected enum enMode
        {
            Add,
            Edit,
            View
        }

        protected enMode Mode;

        public event Action<int>? EntitySaved;

        protected EntityControl(BaseCrudService<T> service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // =============================
        // LOAD METHODS
        // =============================

        public void LoadForCreate()
        {
            Entity = new T();
            EntityId = 0;
            Mode = enMode.Add;

            ResetUI();
            BindEntityToUI();
            ApplyMode();
        }

        public void LoadForEdit(int id)
        {
            Entity = Service.GetById(id).Value
                ?? throw new InvalidOperationException(
                    $"{typeof(T).Name} not found.");

            EntityId = id;
            Mode = enMode.Edit;

            BindEntityToUI();
            ApplyMode();
        }

        public void LoadForView(int id)
        {
            Entity = Service.GetById(id).Value
                ?? throw new InvalidOperationException(
                    $"{typeof(T).Name} not found.");

            EntityId = id;
            Mode = enMode.View;

            BindEntityToUI();
            ApplyMode();
        }

        // =============================
        // SAVE PIPELINE
        // =============================

        public bool Save()
        {
            try
            {
                MapUIToEntity();

                var validation = Service.Validate(Entity);

                if (!validation.IsValid)
                {
                    clsMessage.ShowValidationErrors(validation.Errors);
                    return false;
                }

                if (Mode == enMode.Add)
                {
                    EntityId = Service.Create(Entity).Value;
                }
                else
                {
                    if (!Service.Update(Entity).IsSuccess)
                    {
                        clsMessage.ShowError("Update failed.");
                        return false;
                    }

                    EntityId = GetEntityId(Entity);
                }

                Mode = enMode.View;
                ApplyMode();

                EntitySaved?.Invoke(EntityId);

                clsMessage.ShowSuccess($"{typeof(T).Name} saved successfully.");

                return true;
            }
            catch (Exception ex)
            {
                clsMessage.ShowException("Saving failed.", ex);
                return false;
            }
        }

        // =============================
        // ABSTRACT CONTRACT
        // =============================

        protected abstract void BindEntityToUI();
        protected abstract void MapUIToEntity();
        protected abstract void ResetUI();
        protected abstract void ApplyMode();
        protected abstract int GetEntityId(T entity);
    }


}
