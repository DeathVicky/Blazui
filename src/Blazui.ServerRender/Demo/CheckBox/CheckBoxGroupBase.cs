using Blazui.Component.CheckBox;
using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.ServerRender.Demo.CheckBox
{
    public class CheckBoxGroupBase : ComponentBase
    {
        public Status Status { get; set; }
        public List<string> Values { get; set; }
        public List<string> SelectedValues { get; set; }

        protected override void OnInitialized()
        {
            Values = new List<string>()
{
                    "列表选项1",
                    "列表选项2",
                    "列表选项3"
                };
            SelectedValues = new List<string>()
{
                "列表选项1",
                "列表选项3"
            };
            Status = Status.Indeterminate;
        }
        public void SelectAll(ChangeEventArgs e)
        {
            var args = (BChangeEventArgs<CheckBoxValue>)e;
            Status = args.NewValue.Status;
            if (Status == Status.Checked)
            {
                SelectedValues = new List<string>(Values);
            }
            else if (Status == Status.UnChecked)
            {
                SelectedValues = new List<string>();
            }
            StateHasChanged();
        }
        public void OnSelectAllStatusChagned(ChangeEventArgs e)
        {
            var args = (BChangeEventArgs<CheckBoxValue>)e;
            var status = args.NewValue.Status;
            if (status == Status.Checked)
            {
                SelectedValues.Add(args.NewValue.Value.ToString());
            }
            else if (status == Status.UnChecked)
            {
                SelectedValues.Remove(args.OldValue.Value.ToString());
            }
            if (Values.Count == SelectedValues.Count)
            {
                Status = Status.Checked;
            }
            else if (SelectedValues.Count <= 0)
            {
                Status = Status.UnChecked;
            }
            else if (Values.Count > SelectedValues.Count)
            {
                Status = Status.Indeterminate;
            }
            StateHasChanged();
        }
    }
}
