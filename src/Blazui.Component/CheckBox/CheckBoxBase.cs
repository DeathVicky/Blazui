using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.CheckBox
{
    public class CheckBoxBase<TModel> : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Func<TModel, bool> IsChecked { get; set; }

        [Parameter]
        public Func<TModel, bool> IsIndeterminate { get; set; }

        [Parameter]
        public IList<TModel> SelectedItems { get; set; }

        [Parameter]
        public IList<TModel> For { get; set; }

        [Parameter]
        public Func<TModel, object> TrueValue { get; set; }

        [Parameter]
        public Func<TModel, object> FalseValue { get; set; }

        [Parameter]
        public Func<TModel, string> Label { get; set; }

        protected IList<CheckBoxOption> models { get; set; }

        protected void OnInnerStatusChanged(ChangeEventArgs e)
        {
            var args = (BChangeEventArgs<CheckBoxValue>)e;
            var option = args.NewValue;
            var model = (TModel)Convert.ChangeType(option.Value, typeof(TModel));
            if (SelectedItems != null)
            {
                if (option.Status == Status.Checked)
                {
                    lock (SelectedItems)
                    {
                        if (!SelectedItems.Contains(model))
                        {
                            SelectedItems.Add(model);
                        }
                    }
                }
                else if (option.Status == Status.UnChecked)
                {
                    lock (SelectedItems)
                    {
                        SelectedItems.Remove(model);
                    }
                }
            }
            OnStatusChanged?.Invoke(e);
        }

        [Parameter]
        public Action<ChangeEventArgs> OnStatusChanged { get; set; }
        [Parameter]
        public EventCallback<Status> StatusChanged { get; set; }

        protected bool ModelItemIsSimpleType { get; set; }

        protected override void OnParametersSet()
        {
            if (For == null)
            {
                return;
            }
            var type = typeof(TModel);
            ModelItemIsSimpleType = type.IsValueType || type.IsPrimitive || type == typeof(string);
            if (ModelItemIsSimpleType)
            {
                models = new List<CheckBoxOption>();
                foreach (var item in For)
                {
                    models.Add(ConvertModelItem(item));
                }
            }
        }

        private CheckBoxOption ConvertModelItem(TModel modelItem)
        {
            if (ModelItemIsSimpleType)
            {
                Status status = CheckBox.Status.Checked;
                if (IsChecked != null)
                {
                    status = IsChecked(modelItem) ? Status.Checked : Status.UnChecked;
                }

                if (IsIndeterminate != null)
                {
                    status = IsIndeterminate(modelItem) ? Status.Indeterminate : Status.UnChecked;
                }
                if (SelectedItems != null)
                {
                    status = SelectedItems.Contains(modelItem) ? Status.Checked : Status.UnChecked;
                }

                var label = string.Empty;
                if (Label != null)
                {
                    label = Label(modelItem);
                }
                else if (ModelItemIsSimpleType)
                {
                    label = Convert.ToString(modelItem);
                }
                var option = new CheckBoxOption()
                {
                    Status = status,
                    TrueValue = TrueValue == null ? label : TrueValue(modelItem),
                    FalseValue = FalseValue == null ? string.Empty : FalseValue(modelItem),
                    IsDisabled = false,
                    Label = label
                };
                return option;
            }
            return null;
        }

        public void Render()
        {
            this.StateHasChanged();
        }
    }
}
